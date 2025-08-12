using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpendSmartDbContext _context;

        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expences()
        {
            
            var allExp = _context.Expences.ToList();
            var total = allExp.Sum(x => x.Value);
            ViewBag.Expences = total;
            return View(allExp);
        }

        public IActionResult CreateExpencesEdit(int? id)
        {
            if (id != null)
            {
                var expenceInDb = _context.Expences.SingleOrDefault(x => x.Id == id);
                return View(expenceInDb);
            }
            return View();
        }

        // The View() method that hides the inherited method is removed since it's not used.
        // The code is assumed to be there for future use, but it's unnecessary now.

        public IActionResult CreateExpencesEditForm(Expence model)
        {
            _context.Expences.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Expences");
        }

        // Fixed the Delete method by adding a null check for expenceInDb
        public IActionResult DeleteExpence(int id)
        {
            var expenceInDb = _context.Expences.SingleOrDefault(x => x.Id == id);

            if (expenceInDb != null)
            {
                _context.Expences.Remove(expenceInDb);
                _context.SaveChanges();
            }
            else
            {
                // Handle the case where the Expence was not found (optional)
                _logger.LogWarning($"Expense with id {id} was not found.");
                // Optionally, you can return a View or a Redirect with a message
                // return RedirectToAction("Expences", new { message = "Expence not found" });
            }

            return RedirectToAction("Expences");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
