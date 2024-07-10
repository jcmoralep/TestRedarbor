using Common.Application.Interfaces;
using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;
using Inventory.Application.Products.Queries;
using Inventory.Domain.Entities;

namespace Inventory.Application.Products.Handlers
{
    public class GetAllProductsHandler : IQueryHandler<GetAllProductQuery, List<ProductDto>>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetAllProductsHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductQuery query)
        {
            List<ProductDto> result = new List<ProductDto>();
            var products = await _repository.GetAllAsync();
            if (products.Any())
            {
                result.AddRange(products.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    Status = x.Status,
                    CreationDate = x.CreationDate,
                    ModificationDate = x.ModificationDate
                }));
            }

            return result;
        }
    }
}
