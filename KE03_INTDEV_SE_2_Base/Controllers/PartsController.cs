using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Models;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class PartsController : Controller
    {
        private readonly MatrixIncDbContext _context;

        public PartsController(MatrixIncDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var parts = await _context.Parts.ToListAsync();
            return View(parts);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var part = await _context.Set<Part>().FirstOrDefaultAsync(m => m.Id == id);
            if (part == null) return NotFound();

            return View(part);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price")] Part part)
        {
            if (ModelState.IsValid)
            {
                _context.Add(part);
                await _context.SaveChangesAsync();

                LogChange($"Created part {part.Name} with ID {part.Id} at {DateTime.Now}.", "logFile.txt");
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var part = await _context.Set<Part>().FindAsync(id);
            if (part == null) return NotFound();

            return View(part);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] Part part)
        {
            if (id != part.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(part);
                    await _context.SaveChangesAsync();

                    LogChange($"Edited part {part.Name} with ID {part.Id} at {DateTime.Now}.", "logFile.txt");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartExists(part.Id)) return NotFound();
                    else throw;
                }
            }
            return View(part);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var part = await _context.Set<Part>().FirstOrDefaultAsync(m => m.Id == id);
            if (part == null) return NotFound();

            return View(part);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = await _context.Set<Part>().FindAsync(id);
            if (part != null)
            {
                _context.Set<Part>().Remove(part);
                await _context.SaveChangesAsync();

                LogChange($"Deleted part {part.Name} with ID {part.Id} at {DateTime.Now}.", "logFile.txt");
            }
            return RedirectToAction(nameof(Index));
        }

        private void LogChange(string logMessage, string path)
        {
            if (path != "logFile.txt") path = "logFile.txt";
            var userName = HttpContext?.Session?.GetString("UserName") ?? "Unknown";

            using (StreamWriter writetext = new StreamWriter(path, append: true))
            {
                writetext.WriteLine($"{logMessage}   -   {userName}");
                writetext.Flush();
            }
        }

        private bool PartExists(int id)
        {
            return _context.Set<Part>().Any(e => e.Id == id);
        }
    }
}
