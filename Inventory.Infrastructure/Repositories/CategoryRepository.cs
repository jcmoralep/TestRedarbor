using Inventory.Domain.Entities;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category, AppDbContext>
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext context)
        : base(context)
    {
        _dbContext = context;
    }
}
