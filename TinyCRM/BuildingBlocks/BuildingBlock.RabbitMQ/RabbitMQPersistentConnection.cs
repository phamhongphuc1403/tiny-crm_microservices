using System.Net.Sockets;
using BuildingBlock.Application.Email;
using BuildingBlock.Application.Email.ConstMessageMails;
using BuildingBlock.Infrastructure.RedisCache.Cache.Interface;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace BuildingBlock.RabbitMQ;

public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<RabbitMQPersistentConnection> _logger;
    private readonly int _retryCount;
    private readonly IEmailSender _emailSender;
    private readonly object _syncRoot = new();
    private IConnection _connection;
    private readonly IMailSenderCacheManager _mailSenderCacheManager;
    public bool Disposed;
    private readonly string _subscriptionClientName;

    public RabbitMQPersistentConnection(IConnectionFactory connectionFactory,
        ILogger<RabbitMQPersistentConnection> logger, IEmailSender emailSender,
        IMailSenderCacheManager mailSenderCacheManager, string subscriptionClientName, int retryCount = 5)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _emailSender = emailSender;
        _mailSenderCacheManager = mailSenderCacheManager;
        _subscriptionClientName = subscriptionClientName;
        _retryCount = 2;
        _mailSenderCacheManager.RemoveMailSenderAsync(EmailSubject.RabbitMqConnectionIssue +
                                                      _subscriptionClientName);
    }

    public bool IsConnected => _connection is { IsOpen: true } && !Disposed;

    public IModel CreateModel()
    {
        if (!IsConnected)
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

        var model = _connection.CreateModel();
        _mailSenderCacheManager.RemoveMailSenderAsync(EmailSubject.RabbitMqConnectionIssue +
                                                      _subscriptionClientName);
        return model;
    }

    public bool TryConnect()
    {
        _logger.LogInformation("RabbitMQ Client is trying to connect");

        lock (_syncRoot)
        {
            try
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        (ex, time) =>
                        {
                            _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s",
                                $"{time.TotalSeconds:n1}");
                        }
                    );

                policy.Execute(() =>
                {
                    _connection = _connectionFactory
                        .CreateConnection();
                });

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;
                    _mailSenderCacheManager.RemoveMailSenderAsync(EmailSubject.RabbitMqConnectionIssue +
                                                                  _subscriptionClientName);
                    _logger.LogInformation(
                        "RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events",
                        _connection.Endpoint.HostName);

                    return true;
                }
            }
            catch
            {
                var status = _mailSenderCacheManager
                    .CheckStatusMailSenderAsync(EmailSubject.RabbitMqConnectionIssue + _subscriptionClientName).Result;
                if (status == null)
                {
                    _mailSenderCacheManager.SetStatusMailSenderAsync(EmailSubject.RabbitMqConnectionIssue +
                                                                     _subscriptionClientName).Wait();
                    var emailMessage = new EmailMessage
                    {
                        ToEmails = EmailReceive.RabbitMqConnectionIssue,
                        Subject = EmailSubject.RabbitMqConnectionIssue + " - Service[" + _subscriptionClientName + "]",
                        Content = EmailContent.RabbitMqConnectionIssueContent
                    };
                    _emailSender.SendEmailAsync(emailMessage).Wait();
                }

                _logger.LogCritical("Fatal error: RabbitMQ connections could not be created and opened");
            }

            return false;
        }
    }

    public void Dispose()
    {
        if (Disposed) return;

        Disposed = true;

        try
        {
            _connection.ConnectionShutdown -= OnConnectionShutdown;
            _connection.CallbackException -= OnCallbackException;
            _connection.ConnectionBlocked -= OnConnectionBlocked;
            _connection.Dispose();
        }
        catch (IOException ex)
        {
            _logger.LogCritical(ex.ToString());
        }
    }

    private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
    {
        if (Disposed) return;

        _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

        TryConnect();
    }

    private void OnCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (Disposed) return;

        _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

        TryConnect();
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs reason)
    {
        if (Disposed) return;

        _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");
        TryConnect();
    }
}