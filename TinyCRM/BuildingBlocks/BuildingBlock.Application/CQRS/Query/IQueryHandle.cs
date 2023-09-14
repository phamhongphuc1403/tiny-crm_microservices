using MediatR;

namespace BuildingBlock.Application.CQRS.Query;

public interface IQueryHandle<in TRequest,TResponse>: IRequestHandler<TRequest,TResponse> where TRequest:IQuery<TResponse>
{
    
}