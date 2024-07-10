using Inventory.Domain.Entities;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;

namespace Inventory.Test.Repository
{
    internal class ProductRepository : GenericRepository<Product, AppDbContext>
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
