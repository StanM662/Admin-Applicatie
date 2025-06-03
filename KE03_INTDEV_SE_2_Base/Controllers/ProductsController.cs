using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Models;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MatrixIncDbContext _context;

        public ProductsController(MatrixIncDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Set<Product>().ToListAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Set<Product>().FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            string logMessage = $"Created product {product.Name} with ID {product.Id} at {DateTime.Now}.";
            LogChange(logMessage, "logFile.txt");
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Set<Product>().FindAsync(id);
            if (product == null) return NotFound();

            string logMessage = $"Edited product {product.Name} with ID {product.Id} at {DateTime.Now}.";
            LogChange(logMessage, "logFile.txt");
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            string logMessage = $"Edited product {product.Name} with ID {product.Id} at {DateTime.Now}.";
            LogChange(logMessage, "logFile.txt");
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Set<Product>().FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();
            string logMessage = $"Deleted product {product.Name} with ID {product.Id} at {DateTime.Now}.";
            LogChange(logMessage, "logFile.txt");
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Set<Product>().FindAsync(id);
            if (product != null)
            {
                _context.Set<Product>().Remove(product);
                await _context.SaveChangesAsync();
            }
            string logMessage = $"Deleted product {product.Name} with ID {product.Id} at {DateTime.Now}.";
            LogChange(logMessage, "logFile.txt");
            return RedirectToAction(nameof(Index));
        }

        public void LogChange(string logMessage, string path)
        {
            using (StreamWriter writetext = new StreamWriter(path, append: true))
            {
                writetext.WriteLine($"{logMessage}");
                writetext.Flush();
            }
        }


        private bool ProductExists(int id)
        {
            return _context.Set<Product>().Any(e => e.Id == id);
        }
    }
}
