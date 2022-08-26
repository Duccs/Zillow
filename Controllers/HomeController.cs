using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zillow.Data;
using Zillow.Models;

namespace Zillow.Controllers
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

        public IActionResult Index(string searchString, int location, int property)
        {
            ViewBag.EstateCount = _context.RealEstate.Count();
            ViewBag.AddressCount = _context.Address.Count();
            ViewBag.CustomerCount = _context.Customer.Count();

            ViewData["AddressId"] = new SelectList(_context.Address, "Id", "City");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");

            var estates = from s in _context.RealEstate.Include(r => r.Address).Include(r => r.Category)
                          select s;

            var estatesList = estates.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                //movies = movies.Where(s => s.Title!.Contains(searchString));
                estatesList = estatesList.FindAll(m => m.Name!.Contains(searchString));
            }

            if (location > 0)
            {
                estatesList = estatesList.FindAll(m => m.Address.Id == location);
            }

            if (property > 0)
            {
                estatesList = estatesList.FindAll(m => m.Category.Id == property);
            }

            ViewBag.Estates = estatesList.Skip(0).Take(6).ToList();
            ViewData["images"] = _context.Image.Include(i => i.RealEstate);
            return View();
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