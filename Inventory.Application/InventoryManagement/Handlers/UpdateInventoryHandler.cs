using Common.Application.Interfaces;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;
using Inventory.Application.InventoryManagement.Comands;
using Inventory.Domain.Entities;

namespace Inventory.Application.InventoryManagement.Handlers
{
    public class UpdateInventoryHandler : ICommandHandler<UpdateInventoryCommand, InventoryDto>
    {
        private readonly IGenericRepository<Inventories> _repository;

        public UpdateInventoryHandler(IGenericRepository<Inventories> repository)
        {
            _repository = repository;
        }

        public async Task<InventoryDto> Handle(UpdateInventoryCommand command)
        {
            InventoryDto result = null;
            var inventory = await _repository.GetByIdAsync(command.Id);
            if (inventory == null)
            {
                throw new ArgumentException("Inventory not found");
            }
            inventory.Quantity = command.Quantity;
            inventory.LastUpdated = DateTime.UtcNow;
            var updatedInventory = await _repository.UpdateAsync(inventory);
            if (updatedInventory is not null)
            {
                result = new InventoryDto()
                {
                    Id = updatedInventory.Id,
                    ProductId = updatedInventory.ProductId,
                    Quantity = updatedInventory.Quantity,
                    MovementType = updatedInventory.MovementType,
                    LastUpdated = updatedInventory.LastUpdated
                };
            }

            return result;
        }
    }
}
