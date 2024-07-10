namespace Inventory.Application.CQRS.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand, TResponse> CreateHandler<TCommand, TResponse>() where TCommand : ICommand<TResponse>;
    }
}
