using Inventory.Domain.Entities;
using Common.Application.Enum;
using Microsoft.Extensions.Configuration;
using Inventory.Test.Repository;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Test
{
    [TestClass]
    public class ProductRepositoryTests
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
        public async Task CreateProduct_Valid()
        {
            var dbContext = GetTestDbContext();
            var repository = new ProductRepository(dbContext);
            var product = new Product { Name = "Test Product 1", Price = 10, CategoryId = 1, Status = (int)Status.Active, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow};

            await repository.AddAsync(product);

            var addedProduct = dbContext.Product.FirstOrDefault(p => p.Name == "Test Product 1");
            Assert.IsNotNull(addedProduct);
            Assert.AreEqual(10, addedProduct.Price);
        }

        [TestMethod]
        public async Task GetProductById_ShouldReturnProduct()
        {            
            var context = GetTestDbContext();
            var repository = new ProductRepository(context);
            var product = new Product { Name = "Test Get Product", Price = 9.99m, CategoryId = 1, Status = (int)Status.Active, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow };
            context.Product.Add(product);
            await context.SaveChangesAsync();

            var fetchedProduct = await repository.GetByIdAsync(product.Id);

            Assert.IsNotNull(fetchedProduct);
            Assert.AreEqual(product.Name, fetchedProduct.Name);
        }

        [TestMethod]
        public async Task GetAllProducts_ShouldReturnAllProducts()
        {
            var context = GetTestDbContext();
            var repository = new ProductRepository(context);

            var product1 = new Product { Name = "Product 1", Price = 10m, CategoryId = 1, Status = (int)Status.Active, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow };
            var product2 = new Product { Name = "Product 2", Price = 20m, CategoryId = 2, Status = (int)Status.Active, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow };
            context.Product.AddRange(product1, product2);
            await context.SaveChangesAsync();

            var products = await repository.GetAllAsync();

            Assert.IsNotNull(products);
            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]
        public async Task UpdateProduct_ShouldUpdateProduct()
        {
            var context = GetTestDbContext();
            var repository = new ProductRepository(context);
            var product = new Product { Name = "Test Update Product", Price = 9.99m, CategoryId = 1, Status = (int)Status.Active, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow };
            context.Product.Add(product);
            await context.SaveChangesAsync();

            product.Price = 19.99m;
            await repository.UpdateAsync(product);

            var updatedProduct = await context.Product.FirstOrDefaultAsync(p => p.Id == product.Id);
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual(19.99m, updatedProduct.Price);
        }


        [TestMethod]
        public async Task DeleteProduct_ShouldDeleteProduct()
        {
            var context = GetTestDbContext();
            var repository = new ProductRepository(context);
            var product = new Product { Name = "Test Delete Product", Price = 9.99m, CategoryId = 1, Status = (int)Status.Active, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow };
            context.Product.Add(product);
            await context.SaveChangesAsync();

            await repository.DeleteAsync(product.Id);

            var deletedProduct = await repository.GetByIdAsync(product.Id);
            Assert.IsNotNull(deletedProduct);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            var context = GetTestDbContext();
            context.Product.RemoveRange(context.Product);
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