using DataAccessLayer;
using KE03_INTDEV_SE_2_Base.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;

public class HomeController : Controller
{
    private readonly MatrixIncDbContext _context;

    public HomeController(MatrixIncDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Haal orders en klanten op uit de database
        var orders = _context.Orders.ToList();
        var customers = _context.Customers.ToList();

        var ordersPerDay = orders
            .GroupBy(o => o.OrderDate.Date)
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key.ToString("yyyy-MM-dd"), g => g.Count());

        var customersPerDay = customers
            .GroupBy(c => c.CreatedAt.Date)
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key.ToString("yyyy-MM-dd"), g => g.Count());

        var viewModel = new DashboardViewModel
        {
            TotalCustomers = customers.Count,
            TotalOrders = orders.Count,
            TotalProducts = _context.Products.Count(),
            TotalParts = _context.Parts.Count(),

            Customers = customers,
            Orders = orders,
            Products = _context.Products.ToList(),
            Parts = _context.Parts.ToList(),

            OrdersPerDay = ordersPerDay,
            CustomersPerDay = customersPerDay
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Logs()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
