using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zillow.Data;
using Zillow.Models;

namespace Zillow.Controllers
{
    [Authorize]
    public class RealEstatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RealEstatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RealEstates
        public async Task<IActionResult> Index(string searchString, int location, int property, int page = 1)
        {
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

            const int pageSize = 6;
            if(page < 1)
            {
                page = 1;
            }

            int estateCount = estatesList.Count();
            var pager = new Pager(estateCount, page, pageSize);

            int estateSkip = (page - 1) * pageSize;

            var data = estatesList.Skip(estateSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            ViewData["images"] = _context.Image.Include(i => i.RealEstate);
            return View(data);
        }

        // GET: RealEstates/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RealEstate == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstate
                .Include(r => r.Address)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realEstate == null)
            {
                return NotFound();
            }

            ViewData["Images"] = _context.Image.ToList().FindAll(x => x.RealEstateId == id);

            return View(realEstate);
        }

        // GET: RealEstates/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "Id", "City");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: RealEstates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,Desc,AddressId,CategoryId")] RealEstate realEstate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(realEstate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "Id", "City", realEstate.AddressId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", realEstate.CategoryId);
            return View(realEstate);
        }

        // GET: RealEstates/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RealEstate == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstate.FindAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "Id", "City", realEstate.AddressId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", realEstate.CategoryId);
            return View(realEstate);
        }

        // POST: RealEstates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Desc,AddressId,CategoryId")] RealEstate realEstate)
        {
            if (id != realEstate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realEstate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealEstateExists(realEstate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "Id", "City", realEstate.AddressId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", realEstate.CategoryId);
            return View(realEstate);
        }

        // GET: RealEstates/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RealEstate == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstate
                .Include(r => r.Address)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }

        // POST: RealEstates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RealEstate == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RealEstate'  is null.");
            }
            var realEstate = await _context.RealEstate.FindAsync(id);
            if (realEstate != null)
            {
                _context.RealEstate.Remove(realEstate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealEstateExists(int id)
        {
          return (_context.RealEstate?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
