using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class LoginController : Controller
    {
        private readonly MatrixIncDbContext _context;
        public LoginController(MatrixIncDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            var userName = HttpContext.Session.GetString("UserName") ?? "Unknown";
            LogChange($"Account {userName} logged out at {DateTime.Now}.", "logFile.txt");
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Login([Bind("Id,Name,Password")] Account account)
        {
            if (ModelState.IsValid)
            {
                var matchedAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == account.Name && a.Password == account.Password);

                if (matchedAccount != null)
                {
                    HttpContext.Session.SetString("UserName", matchedAccount.Name);
                    HttpContext.Session.SetString("isLoggedIn", "True");
                    LogChange($"Account {account.Name} logged in at {DateTime.Now}.", "logFile.txt");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    LogChange($"Invalid login attempt for account {account.Name} at {DateTime.Now}.", "logFile.txt");
                }
            }
            return View("Index");
        }


        private void LogChange(string logMessage, string path)
        {
            if (path != "logFile.txt") { path = "logFile.txt"; }

            using (StreamWriter writetext = new StreamWriter(path, append: true))
            {
                writetext.WriteLine(logMessage);
                writetext.Flush();
            }
        }
    }
}
