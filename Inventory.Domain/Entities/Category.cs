namespace Inventory.Domain.Entities;
public class Category : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Category()
    {
        Id = 0;
        Name = string.Empty;
        Description = string.Empty;
        Status = 0;
    }
}
