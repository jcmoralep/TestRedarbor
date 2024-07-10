using Common.Application.Enum;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;

namespace Inventory.Application.InventoryManagement.Comands
{
    public class CreateInventoryCommand : ICommand<InventoryDto>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
    }
}
