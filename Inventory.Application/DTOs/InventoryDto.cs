namespace Inventory.Application.DTOs;
public class InventoryDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int MovementType { get; set; }
    public DateTime LastUpdated { get; set; }
}
