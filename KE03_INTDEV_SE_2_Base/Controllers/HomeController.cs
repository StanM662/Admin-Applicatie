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

<<<<<<< HEAD
        return View(viewModel);
=======
        public IActionResult Index()
        {
            return View();
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
>>>>>>> 2c81667e6208947082015ce8365faece0c4f8c53
    }
}
