using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject.Models;
using System.Diagnostics;

namespace MiniProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult List()
        {
            var audiences = _context.Audiences.ToList(); 

            ViewBag.Total = audiences.Count;
            ViewBag.AverageAge = audiences.Average(a => a.Age);

            return View(audiences);
        }
        public IActionResult Delete(int id)
        {
            var audience = _context.Audiences.Find(id);
            if (audience != null)
            {
                _context.Audiences.Remove(audience);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(List));
        }
        public IActionResult Index()
        {
            ViewData["MovieTitle"] = "Coolie";
            ViewData["Description"] = "Coolie is a thrilling action drama featuring unforgettable performances.";
            ViewData["Director"] = "Mani Ratnam";
            ViewData["ReleaseDate"] = "14 August 2025";
            return View();
        }
        [HttpGet]
        public IActionResult Book()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Book(Audience audience)
        {
            if (ModelState.IsValid)
            {
                _context.Audiences.Add(audience);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(audience);
        }
     
      
        public IActionResult Details(int id)
        {
            var audience = _context.Audiences.FirstOrDefault(a => a.Id == id);
            if (audience == null) return NotFound();
            return View(audience);
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
