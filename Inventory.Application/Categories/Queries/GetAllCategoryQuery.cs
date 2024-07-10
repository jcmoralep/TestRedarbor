using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;

namespace Inventory.Application.Categories.Queries
{
    public class GetAllCategoriesQuery : IQuery<List<CategoryDto>>
    {
    }
}
