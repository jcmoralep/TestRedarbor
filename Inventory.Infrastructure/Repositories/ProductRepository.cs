using Inventory.Domain.Entities;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product, AppDbContext>
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext context)
        : base(context)
    {
        _dbContext = context;
    }
}
