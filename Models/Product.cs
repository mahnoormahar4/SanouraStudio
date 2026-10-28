using System.ComponentModel.DataAnnotations;
namespace SanouraStudio.Models;

public class Product
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public int Price { get; set; }            // PKR
    public string Description { get; set; } = "";
    public bool InStock { get; set; } = true;
}
