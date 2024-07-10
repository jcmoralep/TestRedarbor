using Common.Application.Interfaces;
using Inventory.Application.Categories.Comands;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.Categories.Handlers
{
    public class UpdateCategoryHandler : ICommandHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly IGenericRepository<Category> _repository;

        public UpdateCategoryHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand command)
        {
            CategoryDto result = null;
            var category = await _repository.GetByIdAsync(command.Id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            category.Name = command.Name;
            category.Description = command.Description;
            var updatedCategory = await _repository.UpdateAsync(category);
            if (updatedCategory != null)
            {
                result = new CategoryDto()
                {
                    Id = updatedCategory.Id,
                    Name = updatedCategory.Name,
                    Description = updatedCategory.Description
                };
            }

            return result;
        }
    }
}
