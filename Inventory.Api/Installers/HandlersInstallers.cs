using Inventory.Application.Categories.Comands;
using Inventory.Application.Categories.Handlers;
using Inventory.Application.Categories.Queries;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;
using Inventory.Application.InventoryManagement.Comands;
using Inventory.Application.InventoryManagement.Handlers;
using Inventory.Application.InventoryManagement.Queries;
using Inventory.Application.Products.Comands;
using Inventory.Application.Products.Handlers;
using Inventory.Application.Products.Queries;

namespace Inventory.Api.Installers
{
    public class HandlersInstallers : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICommandHandler<CreateProductCommand, ProductDto>, CreateProductHandler>();
            services.AddTransient<ICommandHandler<UpdateProductCommand, ProductDto>, UpdateProductHandler>();
            services.AddTransient<ICommandHandler<DeleteProductCommand, ProductDto>, DeleteProductHandler>();
            services.AddTransient<IQueryHandler<GetProductByIdQuery, ProductDto>, GetProductByIdHandler>();
            services.AddTransient<IQueryHandler<GetAllProductQuery, List<ProductDto>>, GetAllProductsHandler>();
            
            services.AddTransient<ICommandHandler<CreateCategoryCommand, CategoryDto>, CreateCategoryHandler>();
            services.AddTransient<ICommandHandler<UpdateCategoryCommand, CategoryDto>, UpdateCategoryHandler>();
            services.AddTransient<ICommandHandler<DeleteCategoryCommand, CategoryDto>, DeleteCategoryHandler>();
            services.AddTransient<IQueryHandler<GetCategoryByIdQuery, CategoryDto>, GetCategoryByIdHandler>();
            services.AddTransient<IQueryHandler<GetAllCategoriesQuery, List<CategoryDto>>, GetAllCategoryHandler>();
            
            services.AddTransient<ICommandHandler<CreateInventoryCommand, InventoryDto>, CreateInventoryHandler>();
            services.AddTransient<ICommandHandler<UpdateInventoryCommand, InventoryDto>, UpdateInventoryHandler>();
            services.AddTransient<ICommandHandler<DeleteInventoryCommand, InventoryDto>, DeleteInventoryHandler>();
            services.AddTransient<IQueryHandler<GetInventoryByIdQuery, InventoryDto>, GetInventoryByIdHandler>();
            services.AddTransient<IQueryHandler<GetAllInventoryQuery, List<InventoryDto>>, GetAllInventoryHandler>();
        }
    }
}
