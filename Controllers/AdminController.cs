using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanouraStudio.Data;
using SanouraStudio.Models;
namespace SanouraStudio.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;
    public AdminController(AppDbContext db, IConfiguration config) { _db = db; _config = config; }

    private bool IsAdmin => HttpContext.Session.GetString("admin") == "1";

    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string password)
    {
        if (password == _config["AdminPassword"]) { HttpContext.Session.SetString("admin", "1"); return RedirectToAction(nameof(Index)); }
        ViewBag.Error = "Wrong password.";
        return View();
    }

    public IActionResult Logout() { HttpContext.Session.Remove("admin"); return RedirectToAction("Index", "Home"); }

    public async Task<IActionResult> Index()
    {
        if (!IsAdmin) return RedirectToAction(nameof(Login));
        return View(await _db.Orders.Include(o => o.Items).OrderByDescending(o => o.CreatedAt).ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
    {
        if (!IsAdmin) return RedirectToAction(nameof(Login));
        var order = await _db.Orders.FindAsync(id);
        if (order != null) { order.Status = status; await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }
}
