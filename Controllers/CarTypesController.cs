using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_Management.Data;
using Car_Management.Models;
using Microsoft.AspNetCore.Authorization;

namespace Car_Management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarTypes
        public async Task<IActionResult> Index()
        {
              return _context.CarType != null ? 
                          View(await _context.CarType.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CarType'  is null.");
        }

        // GET: CarTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarType == null)
            {
                return NotFound();
            }

            var carType = await _context.CarType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // GET: CarTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] CarType carType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carType);
        }

        // GET: CarTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarType == null)
            {
                return NotFound();
            }

            var carType = await _context.CarType.FindAsync(id);
            if (carType == null)
            {
                return NotFound();
            }
            return View(carType);
        }

        // POST: CarTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] CarType carType)
        {
            if (id != carType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarTypeExists(carType.Id))
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
            return View(carType);
        }

        // GET: CarTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarType == null)
            {
                return NotFound();
            }

            var carType = await _context.CarType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // POST: CarTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarType'  is null.");
            }
            var carType = await _context.CarType.FindAsync(id);
            if (carType != null)
            {
                _context.CarType.Remove(carType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarTypeExists(int id)
        {
          return (_context.CarType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
