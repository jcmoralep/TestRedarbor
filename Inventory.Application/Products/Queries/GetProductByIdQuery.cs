using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;

namespace Inventory.Application.Products.Queries
{
    public class GetProductByIdQuery : IQuery<ProductDto>
    {
        public int Id { get; set; }
    }
}
