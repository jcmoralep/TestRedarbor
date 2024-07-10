using Inventory.Domain.Entities;    
using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;

namespace Inventory.Application.Categories.Queries
{
    public class GetCategoryByIdQuery : IQuery<CategoryDto>
    {
        public int Id { get; set; }
    }
}
