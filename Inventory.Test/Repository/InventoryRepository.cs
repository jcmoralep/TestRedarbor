using Inventory.Domain.Entities;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;

namespace Inventory.Test.Repository
{
    internal class InventoryRepository : GenericRepository<Inventories, AppDbContext>
    {
        public InventoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
