using Common.Application.Interfaces;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;
using Inventory.Application.Products.Comands;
using Inventory.Domain.Entities;

namespace Inventory.Application.Products.Handlers
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IGenericRepository<Product> _repository;

        public UpdateProductHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand command)
        {
            ProductDto result = null;
            var product = await _repository.GetByIdAsync(command.Id);
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }
            else
            {
                var category = await _repository.GetByIdAsync(command.CategoryId);
                if (category == null)
                {
                    throw new ArgumentException("Category not found");
                }
            }
            product.Name = command.Name;
            product.Price = command.Price;
            product.CategoryId = command.CategoryId;
            product.ModificationDate = DateTime.UtcNow;
            var updateProduct = await _repository.UpdateAsync(product);
            if (updateProduct is not null)
            {
                result = new ProductDto()
                {
                    Id = updateProduct.Id,
                    Name = updateProduct.Name,
                    Price = updateProduct.Price,
                    CategoryId = updateProduct.CategoryId,
                    Status = updateProduct.Status,
                    CreationDate = updateProduct.CreationDate,
                    ModificationDate = updateProduct.ModificationDate
                };
            }

            return result;
        }
    }
}
