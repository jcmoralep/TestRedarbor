using Inventory.Domain.Entities;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;

namespace Inventory.Test.Repository
{
    internal class CategoryRepository : GenericRepository<Category, AppDbContext>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
