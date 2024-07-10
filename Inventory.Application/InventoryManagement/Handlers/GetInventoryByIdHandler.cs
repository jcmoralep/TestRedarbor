using Inventory.Application.CQRS.Queries;
using Inventory.Domain.Entities;
using Inventory.Application.InventoryManagement.Queries;
using Common.Application.Interfaces;
using Inventory.Application.DTOs;

namespace Inventory.Application.InventoryManagement.Handlers
{
    public class GetInventoryByIdHandler : IQueryHandler<GetInventoryByIdQuery, InventoryDto>
    {
        private readonly IGenericRepository<Inventories> _repository;

        public GetInventoryByIdHandler(IGenericRepository<Inventories> repository)
        {
            _repository = repository;
        }

        public async Task<InventoryDto> Handle(GetInventoryByIdQuery query)
        {
            InventoryDto result = null;
            var inventory = await _repository.GetByIdAsync(query.Id);
            if (inventory is null)
            {
                throw new KeyNotFoundException("Inventory not found");
            }
            result = new InventoryDto
            {
                Id = inventory.Id,
                ProductId = inventory.ProductId,
                Quantity = inventory.Quantity,
                LastUpdated = inventory.LastUpdated
            };

            return result;
        }
    }
}
