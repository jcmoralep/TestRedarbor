using Inventory.Application.CQRS.Queries;
using Inventory.Application.Categories.Queries;
using Inventory.Domain.Entities;
using Common.Application.Interfaces;
using Inventory.Application.DTOs;

namespace Inventory.Application.Categories.Handlers
{
    public class GetAllCategoryHandler : IQueryHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly IGenericRepository<Category> _repository;

        public GetAllCategoryHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery query)
        {
            List<CategoryDto> result = new List<CategoryDto>();
            var categories = await _repository.GetAllAsync();
            if (categories.Any())
            {
                result.AddRange(categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }));
            }
            return result;
        }
    }
}
