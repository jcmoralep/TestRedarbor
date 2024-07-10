using Common.Application.Interfaces;
using Inventory.Application.Categories.Comands;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.Categories.Handlers
{
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategoryCommand, CategoryDto>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IGenericRepository<Product> _productCategory;

        public DeleteCategoryHandler(IGenericRepository<Category> repository, IGenericRepository<Product> productCategory)
        {
            _repository = repository;
            _productCategory = productCategory;
        }

        public async Task<CategoryDto> Handle(DeleteCategoryCommand command)
        {
            CategoryDto result = null;
            var category = await _repository.GetByIdAsync(command.Id);
            if (category is null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            var products = await _productCategory.GetAllAsync();
            if (products.Exists(x => x.CategoryId == command.Id))
            {
                throw new ArgumentException("Category is in use and cannot be deleted");
            }
            var deletedCategory = await _repository.DeleteAsync(command.Id);
            if (deletedCategory is not null)
            {
                result = new CategoryDto()
                {
                    Id = deletedCategory.Id,
                    Name = deletedCategory.Name,
                    Description = deletedCategory.Description,
                };
            }

            return result;
        }
    }
}
