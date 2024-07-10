using Inventory.Domain.Entities;
using Common.Application.Enum;
using Microsoft.Extensions.Configuration;
using Inventory.Test.Repository;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Test
{
    [TestClass]
    public class InventoryRepositoryTests
    {
        private static IConfigurationRoot _configuration;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        [TestMethod]
        public async Task CreateInventories_Valid()
        {
            var dbContext = GetTestDbContext();
            var repository = new InventoryRepository(dbContext);
            var inventory = new Inventories { ProductId = 1, Quantity = 10, MovementType = 1, LastUpdated = DateTime.UtcNow, Status = (int)Status.Active };

            await repository.AddAsync(inventory);

            var addedInventory = dbContext.Inventories.FirstOrDefault(i => i.ProductId == 1);
            Assert.IsNotNull(addedInventory);
            Assert.AreEqual(10, addedInventory.Quantity);
        }

        [TestMethod]
        public async Task GetInventoriesById_ShouldReturnInventories()
        {
            var context = GetTestDbContext();
            var repository = new InventoryRepository(context);
            var inventory = new Inventories { ProductId = 2, Quantity = 20, MovementType = 1, LastUpdated = DateTime.UtcNow, Status = (int)Status.Active };
            context.Inventories.Add(inventory);
            await context.SaveChangesAsync();

            var fetchedInventory = await repository.GetByIdAsync(inventory.Id);

            Assert.IsNotNull(fetchedInventory);
            Assert.AreEqual(inventory.ProductId, fetchedInventory.ProductId);
        }

        [TestMethod]
        public async Task GetAllInventories_ShouldReturnAllInventories()
        {
            var context = GetTestDbContext();
            var repository = new InventoryRepository(context);

            var inventory1 = new Inventories { ProductId = 3, Quantity = 30, MovementType = 1, LastUpdated = DateTime.UtcNow, Status = (int)Status.Active };
            var inventory2 = new Inventories { ProductId = 4, Quantity = 40, MovementType = 1, LastUpdated = DateTime.UtcNow, Status = (int)Status.Active };
            context.Inventories.AddRange(inventory1, inventory2);
            await context.SaveChangesAsync();

            var inventories = await repository.GetAllAsync();

            Assert.IsNotNull(inventories);
            Assert.AreEqual(2, inventories.Count);
        }

        [TestMethod]
        public async Task UpdateInventories_ShouldUpdateInventories()
        {
            var context = GetTestDbContext();
            var repository = new InventoryRepository(context);
            var inventory = new Inventories { ProductId = 5, Quantity = 50, MovementType = 1, LastUpdated = DateTime.UtcNow, Status = (int)Status.Active };
            context.Inventories.Add(inventory);
            await context.SaveChangesAsync();

            inventory.Quantity = 60;
            await repository.UpdateAsync(inventory);

            var updatedInventory = await context.Inventories.FirstOrDefaultAsync(i => i.Id == inventory.Id);
            Assert.IsNotNull(updatedInventory);
            Assert.AreEqual(60, updatedInventory.Quantity);
        }

        [TestMethod]
        public async Task DeleteInventories_ShouldDeleteInventories()
        {
            var context = GetTestDbContext();
            var repository = new InventoryRepository(context);
            var inventory = new Inventories { ProductId = 6, Quantity = 70, MovementType = 1, LastUpdated = DateTime.UtcNow, Status = (int)Status.Active };
            context.Inventories.Add(inventory);
            await context.SaveChangesAsync();

            await repository.DeleteAsync(inventory.Id);

            var deletedInventory = await repository.GetByIdAsync(inventory.Id);
            Assert.IsNotNull(deletedInventory);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            var context = GetTestDbContext();
            context.Inventories.RemoveRange(context.Inventories);
            await context.SaveChangesAsync();
        }

        private AppDbContext GetTestDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}