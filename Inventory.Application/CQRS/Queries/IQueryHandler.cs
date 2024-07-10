using Common.Application;

namespace Inventory.Application.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query);
    }
}
