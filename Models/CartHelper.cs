using System.Text.Json;
namespace SanouraStudio.Models;

public static class CartHelper
{
    public static List<CartItem> GetCart(this ISession s) =>
        s.GetString("cart") is { } json ? JsonSerializer.Deserialize<List<CartItem>>(json)! : new();
    public static void SaveCart(this ISession s, List<CartItem> cart) =>
        s.SetString("cart", JsonSerializer.Serialize(cart));
}
