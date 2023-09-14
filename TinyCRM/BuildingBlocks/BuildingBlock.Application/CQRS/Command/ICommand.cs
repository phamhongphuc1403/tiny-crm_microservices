using MediatR;

namespace BuildingBlock.Application.CQRS.Command;

public interface ICommand : IRequest
{
}

public interface ICommand<out T> : IRequest<T>
{
}