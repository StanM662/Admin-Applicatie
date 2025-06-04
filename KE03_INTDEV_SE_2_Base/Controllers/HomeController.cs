using DataAccessLayer;
using KE03_INTDEV_SE_2_Base.ViewModels;
using Microsoft.AspNetCore.Mvc;
using KE03_INTDEV_SE_2_Base.ViewModels; 

public class HomeController : Controller
{
    private readonly MatrixIncDbContext _context;

    public HomeController(MatrixIncDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var viewModel = new DashboardViewModel
        {
            TotalCustomers = _context.Customers.Count(),
            TotalOrders = _context.Orders.Count(),
            TotalProducts = _context.Products.Count(),
            TotalParts = _context.Parts.Count(),

            Customers = _context.Customers.ToList(),
            Orders = _context.Orders.ToList(),
            Products = _context.Products.ToList(),
            Parts = _context.Parts.ToList()
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
