using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;

namespace Inventory.Application.Categories.Comands
{
    public class DeleteCategoryCommand : ICommand<CategoryDto>
    {
        public int Id { get; set; }
    }
}
