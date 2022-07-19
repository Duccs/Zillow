using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zillow.Data;
using Zillow.Models;

namespace Zillow.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RealEstatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RealEstates
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RealEstate.Include(r => r.Address).Include(r => r.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RealEstates/Details/5
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

            return View(realEstate);
        }

        // GET: RealEstates/Create
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
