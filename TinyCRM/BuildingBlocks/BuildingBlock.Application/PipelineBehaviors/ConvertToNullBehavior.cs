using BuildingBlock.Application.CQRS.Command;
using MediatR;

namespace BuildingBlock.Application.PipelineBehaviors;

public class ConvertToNullBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var properties = typeof(TRequest).GetProperties();

        foreach (var property in properties)
        {
            if (property.PropertyType != typeof(string)) continue;

            var value = (string)property.GetValue(request)!;

            if (string.IsNullOrWhiteSpace(value)) property.SetValue(request, null);
        }

        return await next();
    }
}