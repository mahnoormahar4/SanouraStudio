using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SanouraStudio.Models;

public enum OrderStatus { Received, Making, Packed, Shipped, Delivered }

public class Order
{
    public int Id { get; set; }
    public string OrderCode { get; set; } = "";
    [Required] public string CustomerName { get; set; } = "";
    [Required, Phone] public string Phone { get; set; } = "";
    [Required] public string Address { get; set; } = "";
    [Required] public string City { get; set; } = "";
    public string? Notes { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Received;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<OrderItem> Items { get; set; } = new();
    [NotMapped] public int Total => Items.Sum(i => i.Price * i.Quantity);
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; } = "";
    public string Details { get; set; } = "";   // e.g. custom bracelet colours/charm/initial
    public int Price { get; set; }
    public int Quantity { get; set; } = 1;
}

// Lives in the session only (not stored in the database)
public class CartItem
{
    public string Name { get; set; } = "";
    public string Details { get; set; } = "";
    public int Price { get; set; }
    public int Quantity { get; set; } = 1;
}
