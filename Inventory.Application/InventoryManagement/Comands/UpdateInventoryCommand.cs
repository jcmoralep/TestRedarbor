using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;

namespace Inventory.Application.InventoryManagement.Comands
{
    public class UpdateInventoryCommand : ICommand<InventoryDto>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
