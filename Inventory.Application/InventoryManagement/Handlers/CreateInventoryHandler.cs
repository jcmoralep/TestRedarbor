using Common.Application.Enum;
using Common.Application.Interfaces;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;
using Inventory.Application.InventoryManagement.Comands;
using Inventory.Domain.Entities;

namespace Inventory.Application.InventoryManagement.Handlers
{
    public class CreateInventoryHandler : ICommandHandler<CreateInventoryCommand, InventoryDto>
    {
        private readonly IGenericRepository<Inventories> _repository;
        private readonly IGenericRepository<Product> _productRepository;

        public CreateInventoryHandler(IGenericRepository<Inventories> repository, IGenericRepository<Product> productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<InventoryDto> Handle(CreateInventoryCommand command)
        {
            InventoryDto result = null;
            var product = await _productRepository.GetByIdAsync(command.ProductId);
            if (product is null || product.Status == (int)Status.Deleted)
            {
                throw new Exception("Product not found");
            }
            var inventories = new Inventories
            {
                ProductId = command.ProductId,
                Quantity = command.Quantity,
                MovementType = (int)command.MovementType,
                LastUpdated = DateTime.UtcNow,
                Status = (int)Status.Active
            };
            var inventoryCreated = await _repository.AddAsync(inventories);
            if (inventoryCreated is not null)
            {
                result = new InventoryDto()
                {
                    Id = inventoryCreated.Id,
                    ProductId = inventoryCreated.ProductId,
                    Quantity = inventoryCreated.Quantity,
                    MovementType = inventoryCreated.MovementType,
                    LastUpdated = inventoryCreated.LastUpdated
                };
            }

            return result;
        }
    }
}
