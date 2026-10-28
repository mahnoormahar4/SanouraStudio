using Microsoft.EntityFrameworkCore;
using SanouraStudio.Models;
namespace SanouraStudio.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Pastel Bead Bracelet", Category = "Bracelets", Price = 900, Description = "Handmade beaded bracelet in soft pastel colours." },
            new Product { Id = 2, Name = "Charmed Sparkles Bracelet", Category = "Bracelets", Price = 1300, Description = "Bracelet with sparkly charms." },
            new Product { Id = 3, Name = "Crochet Keychain", Category = "Keychains", Price = 700, Description = "Cute crochet keychain, made by hand." },
            new Product { Id = 4, Name = "Birthday Gift Set", Category = "Gift Sets", Price = 3000, Description = "Bracelet, keychain and a note card in a gift pouch." });
    }
}
