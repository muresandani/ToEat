namespace ToEat.Models;

public class Inventory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<InventoryItem> InventoryItems { get; set; }
}
