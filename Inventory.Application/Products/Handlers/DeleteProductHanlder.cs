using Common.Application.Interfaces;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.DTOs;
using Inventory.Application.Products.Comands;
using Inventory.Domain.Entities;

namespace Inventory.Application.Products.Handlers
{
    public class DeleteProductHandler : ICommandHandler<DeleteProductCommand, ProductDto>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IGenericRepository<Inventories> _inventoryRepository;

        public DeleteProductHandler(IGenericRepository<Product> repository, IGenericRepository<Inventories> inventoryRepository)
        {
            _repository = repository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ProductDto> Handle(DeleteProductCommand command)
        {
            ProductDto result = null;
            var product = await _repository.GetByIdAsync(command.Id);
            if (product is null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            var inventories = await _inventoryRepository.GetAllAsync();
            if (inventories.Exists(x => x.ProductId == command.Id))
            {
                throw new Exception("Product is in use and cannot be deleted");
            }
            var deletedProduct = await _repository.DeleteAsync(command.Id);
            if(deletedProduct is not null)
            {
                result = new ProductDto()
                {
                    Id = deletedProduct.Id,
                    Name = deletedProduct.Name,
                    Price = deletedProduct.Price,
                    CategoryId = deletedProduct.CategoryId,
                    Status = deletedProduct.Status,
                    CreationDate = deletedProduct.CreationDate,
                    ModificationDate = deletedProduct.ModificationDate
                };
            }

            return result;
        }
    }
}
