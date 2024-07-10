using Inventory.Application.CQRS.Queries;
using Inventory.Domain.Entities;
using Inventory.Application.Products.Queries;
using Common.Application.Interfaces;
using Inventory.Application.DTOs;

namespace Inventory.Application.Products.Handlers
{
    public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetProductByIdHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery query)
        {
            ProductDto result = null;
            var product = await _repository.GetByIdAsync(query.Id);
            if (product is null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            result = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Status = product.Status,
                CreationDate = product.CreationDate,
                ModificationDate = product.ModificationDate
            };

            return result;
        }
    }
}
