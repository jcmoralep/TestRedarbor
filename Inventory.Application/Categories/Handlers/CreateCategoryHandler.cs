using Common.Application.Enum;
using Common.Application.Interfaces;
using Inventory.Application.Categories.Comands;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.Categories.Handlers
{
    public class CreateCategoryHandler : ICommandHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly IGenericRepository<Category> _repository;

        public CreateCategoryHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand command)
        {
            CategoryDto result = null;
            var category = new Category
            {
                Name = command.Name,
                Description = command.Description,
                Status = (int)Status.Active
            };
            var categoryCrated = await _repository.AddAsync(category);
            if (categoryCrated is not null)
            {
                result = new CategoryDto()
                {
                    Id = categoryCrated.Id,
                    Name = categoryCrated.Name,
                    Description = categoryCrated.Description,
                };
            }

            return result;
        }
    }
}
