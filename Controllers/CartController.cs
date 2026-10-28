using Microsoft.AspNetCore.Mvc;
using SanouraStudio.Models;
namespace SanouraStudio.Controllers;

public class CartController : Controller
{
    public IActionResult Index() => View(HttpContext.Session.GetCart());

    [HttpPost]
    public IActionResult Remove(int index)
    {
        var cart = HttpContext.Session.GetCart();
        if (index >= 0 && index < cart.Count) cart.RemoveAt(index);
        HttpContext.Session.SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }
}
