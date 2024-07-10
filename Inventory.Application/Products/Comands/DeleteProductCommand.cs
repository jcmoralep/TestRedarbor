using Common.Application.Enum;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;

namespace Inventory.Application.Products.Comands
{
    public class DeleteProductCommand : ICommand<ProductDto>
    {
        public int Id { get; set; }
    }
}
