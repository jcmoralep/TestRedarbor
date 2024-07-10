using Common.Application.Interfaces;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.Products.Comands;
using Inventory.Domain.Entities;
using Common.Application.Enum;
using Inventory.Application.DTOs;

namespace Inventory.Application.Products.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProductCommand, ProductDto>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IGenericRepository<Category> _categoryRepository;

        public CreateProductHandler(IGenericRepository<Product> repository, IGenericRepository<Category> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductDto> Handle(CreateProductCommand command)
        {
            ProductDto result = null;
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId);
            if (category is null || category.Status == (int)Status.Deleted)
            {
                throw new Exception("Category not found");
            }
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price,
                CategoryId = command.CategoryId,
                Status = (int)Status.Active,
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow
            };
            var createdproduct = await _repository.AddAsync(product);
            if (createdproduct is not null)
            {
                result = new ProductDto()
                {
                    Id = createdproduct.Id,
                    Name = createdproduct.Name,
                    Price = createdproduct.Price,
                    CategoryId = createdproduct.CategoryId,
                    Status = createdproduct.Status,
                    CreationDate = createdproduct.CreationDate,
                    ModificationDate = createdproduct.ModificationDate
                };
            }

            return result;
        }
    }
}
