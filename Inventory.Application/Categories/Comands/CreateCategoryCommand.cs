using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;

namespace Inventory.Application.Categories.Comands
{
    public class CreateCategoryCommand : ICommand<CategoryDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
