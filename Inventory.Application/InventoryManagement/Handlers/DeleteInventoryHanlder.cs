using Common.Application.Interfaces;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;
using Inventory.Application.InventoryManagement.Comands;
using Inventory.Domain.Entities;

namespace Inventory.Application.InventoryManagement.Handlers
{
    public class DeleteInventoryHandler : ICommandHandler<DeleteInventoryCommand, InventoryDto>
    {
        private readonly IGenericRepository<Inventories> _repository;

        public DeleteInventoryHandler(IGenericRepository<Inventories> repository)
        {
            _repository = repository;
        }

        public async Task<InventoryDto> Handle(DeleteInventoryCommand command)
        {
            InventoryDto result = null;
            var inventory = await _repository.GetByIdAsync(command.Id);
            if (inventory is null)
            {
                throw new KeyNotFoundException("Inventory not found");
            }
            var deletedInventory = await _repository.DeleteAsync(command.Id);
            if (deletedInventory is not null)
            {
                result = new InventoryDto()
                {
                    Id = deletedInventory.Id,
                    ProductId = deletedInventory.ProductId,
                    Quantity = deletedInventory.Quantity,
                    MovementType = deletedInventory.MovementType,
                    LastUpdated = deletedInventory.LastUpdated
                };
            }

            return result;
        }
    }
}
