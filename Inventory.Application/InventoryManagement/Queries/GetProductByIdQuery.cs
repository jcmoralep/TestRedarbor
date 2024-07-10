using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;

namespace Inventory.Application.InventoryManagement.Queries
{
    public class GetInventoryByIdQuery : IQuery<InventoryDto>
    {
        public int Id { get; set; }
    }
}
