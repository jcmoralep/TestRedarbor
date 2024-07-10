using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;

namespace Inventory.Application.InventoryManagement.Comands
{
    public class DeleteInventoryCommand : ICommand<InventoryDto>
    {
        public int Id { get; set; }
    }
}
