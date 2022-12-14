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
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "customer_desc" : "Customer";
            ViewBag.EstateSortParm = sortOrder == "Estate" ? "estate_desc" : "Estate";

            var contracts = from s in _context.Contract.Include(c => c.Customer).Include(c => c.RealEstate)
                            select s;

            switch (sortOrder)
            {
                case "type_desc":
                    contracts = contracts.OrderByDescending(s => s.Type);
                    break;
                case "Price":
                    contracts = contracts.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    contracts = contracts.OrderByDescending(s => s.Price);
                    break;
                case "Customer":
                    contracts = contracts.OrderBy(s => s.Customer.Name);
                    break;
                case "customer_desc":
                    contracts = contracts.OrderByDescending(s => s.Customer.Name);
                    break;
                case "Estate":
                    contracts = contracts.OrderBy(s => s.RealEstate.Name);
                    break;
                case "estate_desc":
                    contracts = contracts.OrderByDescending(s => s.RealEstate.Name);
                    break;
                default:
                    contracts = contracts.OrderBy(s => s.Type);
                    break;
            }
            var contract = contracts.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                //movies = movies.Where(s => s.Title!.Contains(searchString));
                contract = contract.FindAll(m => m.Customer.Name!.Contains(searchString) || m.RealEstate.Name!.Contains(searchString));
            }

            return _context.Address != null ?
                          View(contract) :
                          Problem("Entity set 'ApplicationDbContext.Address'  is null.");
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contract == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract
                .Include(c => c.Customer)
                .Include(c => c.RealEstate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name");
            ViewData["RealEstateId"] = new SelectList(_context.RealEstate, "Id", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Price,CustomerId,RealEstateId")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", contract.CustomerId);
            ViewData["RealEstateId"] = new SelectList(_context.RealEstate, "Id", "Name", contract.RealEstateId);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contract == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", contract.CustomerId);
            ViewData["RealEstateId"] = new SelectList(_context.RealEstate, "Id", "Name", contract.RealEstateId);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Price,CustomerId,RealEstateId")] Contract contract)
        {
            if (id != contract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", contract.CustomerId);
            ViewData["RealEstateId"] = new SelectList(_context.RealEstate, "Id", "Name", contract.RealEstateId);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contract == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract
                .Include(c => c.Customer)
                .Include(c => c.RealEstate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contract == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Contract'  is null.");
            }
            var contract = await _context.Contract.FindAsync(id);
            if (contract != null)
            {
                _context.Contract.Remove(contract);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
          return (_context.Contract?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
