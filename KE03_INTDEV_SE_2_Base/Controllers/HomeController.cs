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
            TotalParts = _context.Parts.Count()
        };

        return View(viewModel);
    }
}
