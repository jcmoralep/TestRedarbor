using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;

namespace Inventory.Application.Products.Comands
{
    public class UpdateProductCommand : ICommand<ProductDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
