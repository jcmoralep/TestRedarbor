using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;

namespace Inventory.Application.InventoryManagement.Queries
{
    public class GetAllInventoryQuery : IQuery<List<InventoryDto>>
    {
    }
}
