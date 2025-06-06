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
                    HttpContext.Session.SetString("Password", matchedAccount.Password);
                    HttpContext.Session.SetString("isLoggedIn", "True");
                    LogChange($"Account {account.Name} logged in at {DateTime.Now}.", "logFile.txt");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    LogChange($"Invalid login attempt for account {account.Name} at {DateTime.Now}.", "logFile.txt");
                }
            }
            return View("Home/Index");
        }

        public async Task<IActionResult> EditName(string Name, string Password, string NewName)
        {
            if (string.IsNullOrWhiteSpace(NewName))
            {
                ModelState.AddModelError("NewName", "Nieuwe naam mag niet leeg zijn.");
                return View("Index");
            }

            var matchedAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Name == Name && a.Password == Password);

            if (matchedAccount == null)
            {
                ModelState.AddModelError("NewName", "Ongeldige gebruikersnaam of wachtwoord.");
                return View("Index");
            }

            if (_context.Accounts.Any(a => a.Name == NewName))
            {
                ModelState.AddModelError("NewName", "Deze naam is al in gebruik.");
                return View("Index");
            }

            string oldName = matchedAccount.Name;
            matchedAccount.Name = NewName;
            await _context.SaveChangesAsync();

            LogChange($"Account {oldName} changed its name to {NewName} at {DateTime.Now}.", "logFile.txt");

            HttpContext.Session.SetString("UserName", NewName);
            HttpContext.Session.SetString("isLoggedIn", "True");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditPassword(string Name, string Password, string NewPassword, string OldPassword)
        {
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                ModelState.AddModelError("NewPassword", "Nieuw wachtwoord mag niet leeg zijn.");
                return View("Index");
            }

            var matchedAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Name == Name && a.Password == Password);

            if (matchedAccount == null)
            {
                ModelState.AddModelError("NewPassword", "Ongeldige gebruikersnaam of wachtwoord.");
                return View("Index");
            }
            
            string _OldPassword = matchedAccount.Password;
            if (OldPassword != _OldPassword)
            {
                ModelState.AddModelError("NewPassword", "Oud wachtwoord komt niet overeen.");
                return View("Index");
            }
            else
            {
                matchedAccount.Password = NewPassword;
                await _context.SaveChangesAsync();

                LogChange($"Account {matchedAccount.Name} changed its password at {DateTime.Now}.", "logFile.txt");

                HttpContext.Session.SetString("Password", NewPassword);
                HttpContext.Session.SetString("isLoggedIn", "True");

                return RedirectToAction(nameof(Index));
            }
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
