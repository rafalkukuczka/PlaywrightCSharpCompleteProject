using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleMvcApp.Data;
using SampleMvcApp.Models;

namespace SampleMvcApp.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _db;

    public OrdersController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _db.Orders
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync();

        return View(orders);
    }

    [HttpGet]
    public IActionResult Create() => View(new Order());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }

        order.CreatedAtUtc = DateTime.UtcNow;
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = "Order created successfully";
        return RedirectToAction(nameof(Index));
    }
}
