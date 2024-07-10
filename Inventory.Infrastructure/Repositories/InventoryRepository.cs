using Inventory.Domain.Entities;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories;

public class InventoryRepository : GenericRepository<Inventories, AppDbContext>
{
    private readonly AppDbContext _dbContext;

    public InventoryRepository(AppDbContext context)
        : base(context)
    {
        _dbContext = context;
    }
}
