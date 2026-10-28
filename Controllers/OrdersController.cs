using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanouraStudio.Data;
using SanouraStudio.Models;
namespace SanouraStudio.Controllers;

public class OrdersController : Controller
{
    private readonly AppDbContext _db;
    public OrdersController(AppDbContext db) => _db = db;

    public IActionResult Checkout()
    {
        if (HttpContext.Session.GetCart().Count == 0) return RedirectToAction("Index", "Cart");
        return View(new Order());
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(Order order)
    {
        var cart = HttpContext.Session.GetCart();
        if (cart.Count == 0) return RedirectToAction("Index", "Cart");
        ModelState.Remove(nameof(Order.OrderCode));
        if (!ModelState.IsValid) return View(order);

        order.OrderCode = "SAN-" + Random.Shared.Next(10000, 99999);
        order.Status = OrderStatus.Received;
        order.CreatedAt = DateTime.Now;
        order.Items = cart.Select(c => new OrderItem { Name = c.Name, Details = c.Details, Price = c.Price, Quantity = c.Quantity }).ToList();
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        HttpContext.Session.Remove("cart");
        return RedirectToAction(nameof(Track), new { code = order.OrderCode });
    }

    public async Task<IActionResult> Track(string? code)
    {
        if (string.IsNullOrWhiteSpace(code)) return View(null);
        var order = await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderCode == code.Trim().ToUpper());
        if (order == null) ViewBag.NotFound = true;
        return View(order);
    }
}
