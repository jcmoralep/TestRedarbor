using Inventory.Domain.Entities;
using Common.Application.Enum;
using Microsoft.Extensions.Configuration;
using Inventory.Test.Repository;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Test
{
    [TestClass]
    public class CategoryRepositoryTests
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
        public async Task CreateCategory_Valid()
        {
            var dbContext = GetTestDbContext();
            var repository = new CategoryRepository(dbContext);
            var category = new Category { Name = "Test Category 1", Description = "Description 1", Status = (int)Status.Active };

            await repository.AddAsync(category);

            var addedCategory = dbContext.Category.FirstOrDefault(c => c.Name == "Test Category 1");
            Assert.IsNotNull(addedCategory);
            Assert.AreEqual("Description 1", addedCategory.Description);
        }

        [TestMethod]
        public async Task GetCategoryById_ShouldReturnCategory()
        {
            var context = GetTestDbContext();
            var repository = new CategoryRepository(context);
            var category = new Category { Name = "Test Get Category", Description = "Description 2", Status = (int)Status.Active };
            context.Category.Add(category);
            await context.SaveChangesAsync();

            var fetchedCategory = await repository.GetByIdAsync(category.Id);

            Assert.IsNotNull(fetchedCategory);
            Assert.AreEqual(category.Name, fetchedCategory.Name);
        }

        [TestMethod]
        public async Task GetAllCategories_ShouldReturnAllCategories()
        {
            var context = GetTestDbContext();
            var repository = new CategoryRepository(context);

            var category1 = new Category { Name = "Category 1", Description = "Description 1", Status = (int)Status.Active };
            var category2 = new Category { Name = "Category 2", Description = "Description 2", Status = (int)Status.Active };
            context.Category.AddRange(category1, category2);
            await context.SaveChangesAsync();

            var categories = await repository.GetAllAsync();

            Assert.IsNotNull(categories);
            Assert.AreEqual(2, categories.Count);
        }

        [TestMethod]
        public async Task UpdateCategory_ShouldUpdateCategory()
        {
            var context = GetTestDbContext();
            var repository = new CategoryRepository(context);
            var category = new Category { Name = "Test Update Category", Description = "Description 3", Status = (int)Status.Active };
            context.Category.Add(category);
            await context.SaveChangesAsync();

            category.Description = "Updated Description";
            await repository.UpdateAsync(category);

            var updatedCategory = await context.Category.FirstOrDefaultAsync(c => c.Id == category.Id);
            Assert.IsNotNull(updatedCategory);
            Assert.AreEqual("Updated Description", updatedCategory.Description);
        }

        [TestMethod]
        public async Task DeleteCategory_ShouldDeleteCategory()
        {
            var context = GetTestDbContext();
            var repository = new CategoryRepository(context);
            var category = new Category { Name = "Test Delete Category", Description = "Description 4", Status = (int)Status.Active };
            context.Category.Add(category);
            await context.SaveChangesAsync();

            await repository.DeleteAsync(category.Id);

            var deletedCategory = await repository.GetByIdAsync(category.Id);
            Assert.IsNotNull(deletedCategory);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            var context = GetTestDbContext();
            context.Category.RemoveRange(context.Category);
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