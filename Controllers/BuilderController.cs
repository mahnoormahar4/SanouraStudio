using Microsoft.AspNetCore.Mvc;
using SanouraStudio.Models;
namespace SanouraStudio.Controllers;

public class BuilderController : Controller
{
    public const int BasePrice = 1200, CharmPrice = 100, InitialPrice = 50;
    public static readonly string[] Colors = { "Pink", "Lavender", "Mint", "Sky Blue", "Peach", "White", "Yellow" };
    public static readonly string[] Charms = { "None", "Heart", "Star", "Butterfly", "Flower" };

    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Add(List<string> colors, string charm, string? initial)
    {
        colors = colors.Where(c => Colors.Contains(c)).ToList();
        if (colors.Count == 0 || colors.Count > 4 || !Charms.Contains(charm))
        {
            TempData["Error"] = "Pick 1 to 4 colours and a charm.";
            return RedirectToAction(nameof(Index));
        }
        initial = string.IsNullOrWhiteSpace(initial) ? null : initial.Trim().Substring(0, 1).ToUpper();
        var price = BasePrice + (charm != "None" ? CharmPrice : 0) + (initial != null ? InitialPrice : 0);
        var details = $"Colours: {string.Join(", ", colors)} | Charm: {charm} | Initial: {initial ?? "-"}";

        var cart = HttpContext.Session.GetCart();
        cart.Add(new CartItem { Name = "Custom Bracelet", Details = details, Price = price });
        HttpContext.Session.SaveCart(cart);
        return RedirectToAction("Index", "Cart");
    }
}
