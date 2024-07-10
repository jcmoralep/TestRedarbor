using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application.CQRS.Queries
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IQueryHandler<TQuery, TResponse> CreateHandler<TQuery, TResponse>() where TQuery : IQuery<TResponse>
        {
            return _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();
        }
    }
}
