using Inventory.Application.CQRS.Commands;
using Inventory.Application.CQRS.Queries;

namespace Inventory.Api.Installers
{
    public class FactoryInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddTransient<IQueryHandlerFactory, QueryHandlerFactory>();
        }
    }
}
