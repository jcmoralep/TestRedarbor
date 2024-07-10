namespace Inventory.Domain.Entities;
public class Inventories : BaseEntity
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int MovementType { get; set; }
    public DateTime LastUpdated { get; set; }

    public Inventories()
    {
        Id = 0;
        ProductId = 0;
        Quantity = 0;
        Status = 0;
        MovementType = 0;
        LastUpdated = DateTime.Now;
    }
}
