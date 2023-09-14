using MediatR;

namespace BuildingBlock.Application.CQRS.Query;

public interface IQuery : IRequest
{
}

public interface IQuery<out T> : IRequest<T>
{
}