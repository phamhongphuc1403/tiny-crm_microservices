using MediatR;

namespace BuildingBlock.Application.CQRS.Command;

public interface ICommandHandler<in TRequest,TResponse>: IRequestHandler<TRequest,TResponse> where TRequest:ICommand<TResponse>
{
}

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
{
    
}