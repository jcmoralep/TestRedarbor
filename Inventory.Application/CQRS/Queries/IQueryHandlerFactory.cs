namespace Inventory.Application.CQRS.Queries
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler<TQuery, TResponse> CreateHandler<TQuery, TResponse>() where TQuery : IQuery<TResponse>;
    }
}
