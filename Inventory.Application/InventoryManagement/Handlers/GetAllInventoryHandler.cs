using Common.Application.Interfaces;
using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;
using Inventory.Application.InventoryManagement.Queries;
using Inventory.Domain.Entities;

namespace Inventory.Application.InventoryManagement.Handlers
{
    public class GetAllInventoryHandler : IQueryHandler<GetAllInventoryQuery, List<InventoryDto>>
    {
        private readonly IGenericRepository<Inventories> _repository;

        public GetAllInventoryHandler(IGenericRepository<Inventories> repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryDto>> Handle(GetAllInventoryQuery query)
        {
            List<InventoryDto> result = new List<InventoryDto>();
            var inventories = await _repository.GetAllAsync();
            if (inventories.Any())
            {
                result.AddRange(inventories.Select(x => new InventoryDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    LastUpdated = x.LastUpdated
                }));
            }

            return result;
        }
    }
}
