using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application.CQRS.Commands
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler<TCommand, TResponse> CreateHandler<TCommand, TResponse>() where TCommand : ICommand<TResponse>
        {
            return _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>();
        }
    }
}
