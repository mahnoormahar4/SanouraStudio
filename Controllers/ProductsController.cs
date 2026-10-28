using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanouraStudio.Data;
using SanouraStudio.Models;
namespace SanouraStudio.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _db;
    public ProductsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? category)
    {
        var q = _db.Products.AsQueryable();
        if (!string.IsNullOrEmpty(category)) q = q.Where(p => p.Category == category);
        ViewBag.Categories = await _db.Products.Select(p => p.Category).Distinct().ToListAsync();
        return View(await q.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null || !p.InStock) return NotFound();
        var cart = HttpContext.Session.GetCart();
        var existing = cart.FirstOrDefault(c => c.Name == p.Name && c.Details == "");
        if (existing != null) existing.Quantity++;
        else cart.Add(new CartItem { Name = p.Name, Price = p.Price });
        HttpContext.Session.SaveCart(cart);
        return RedirectToAction("Index", "Cart");
    }
}
