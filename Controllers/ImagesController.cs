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
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Zillow.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hosting;

        public ImagesController(ApplicationDbContext context, IHostingEnvironment hosting)
        {
            _context = context;
            this.hosting = hosting;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Image.Include(i => i.RealEstate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.RealEstate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            ViewData["RealEstateId"] = new SelectList(_context.Set<RealEstate>(), "Id", "Name");
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Image image)
        {
            if(image.Img != null)
                {
                //ImageUploading
                string ImageFolder = Path.Combine(hosting.WebRootPath, "imgs");
                string ImagePath = Path.Combine(ImageFolder, image.Img.FileName);
                image.Img.CopyTo(new FileStream(ImagePath, FileMode.Create));
                image.ImagePath = image.Img.FileName;
            }
            _context.Add(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["RealEstateId"] = new SelectList(_context.Set<RealEstate>(), "Id", "Name", image.RealEstateId);
            return View(image);
        }

        // GET: Images/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            ViewData["RealEstateId"] = new SelectList(_context.Set<RealEstate>(), "Id", "Name", image.RealEstateId);
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            string oldPath = _context.Image.FirstOrDefault(c => c.Id == id).ImagePath;

            try
            {
                if (image.Img != null)
                {

                    //ImageUploading
                    string ImageFolder = Path.Combine(hosting.WebRootPath, "imgs");
                    string ImagePath = Path.Combine(ImageFolder, image.Img.FileName);
                    image.Img.CopyTo(new FileStream(ImagePath, FileMode.Create));
                    FileInfo file = new FileInfo(Path.Combine(ImageFolder, oldPath));
                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                    }
                    image.ImagePath = image.Img.FileName;

                }

                _context.Update(image);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(image.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["RealEstateId"] = new SelectList(_context.Set<RealEstate>(), "Id", "Name", image.RealEstateId);
            return View(image);
        }

        // GET: Images/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.RealEstate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Image == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Image'  is null.");
            }
            var image = await _context.Image.FindAsync(id);
            if (image != null)
            {
                _context.Image.Remove(image);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
          return (_context.Image?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
