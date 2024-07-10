namespace Inventory.Domain.Entities;
public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }

    public Product()
    {
        Id = 0;
        Name = string.Empty;
        Price = 0;
        CategoryId = 0;
        Status = 0;
        CreationDate = DateTime.Now;
        ModificationDate = DateTime.Now;
    }
}
