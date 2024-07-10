using Inventory.Application.CQRS.Queries;
using Inventory.Domain.Entities;
using Inventory.Application.Categories.Queries;
using Common.Application.Interfaces;
using Inventory.Application.DTOs;

namespace Inventory.Application.Categories.Handlers
{
    public class GetCategoryByIdHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IGenericRepository<Category> _repository;

        public GetCategoryByIdHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery query)
        {
            CategoryDto result = null;
            var category = await _repository.GetByIdAsync(query.Id);
            if (category is null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            result = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return result;
        }
    }
}
