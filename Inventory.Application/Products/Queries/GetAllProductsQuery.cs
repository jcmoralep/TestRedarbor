using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;

namespace Inventory.Application.Products.Queries
{
    public class GetAllProductQuery : IQuery<List<ProductDto>>
    {
    }
}
