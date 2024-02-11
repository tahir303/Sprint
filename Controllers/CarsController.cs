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
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration config;
        public CarsController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            this.config = config;
        }
        [Authorize(Roles = "Customer, Admin")]
        //[Authorize(Roles = "Admin")]
        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Car.Include(c => c.CarTransmissionType).Include(c => c.CarType).Include(c => c.Manufacturer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.CarTransmissionType)
                .Include(c => c.CarType)
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize(Roles ="Admin")]
        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.CarTransmissionType, "Id", "Name");
            ViewData["CarTypeId"] = new SelectList(_context.CarType, "Id", "Type");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            return View();
        }
        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Model,ManufacturerId,CarTypeId,Engine,BHP,CarTransmissionTypeId,Mileage,Seats,AirBagDetails,BootSpace,Price")] Car car)
        {
            //car = _context.Car.Include(c => c.CarTransmissionType).Include(c => c.CarType).Include(c => c.Manufacturer);
            //bool _isuploaded = await HelperCar.UploadBlob(config, car);
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.CarTransmissionType, "Id", "Name", car.CarTransmissionTypeId);
            ViewData["CarTypeId"] = new SelectList(_context.CarType, "Id", "Type", car.CarTypeId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", car.ManufacturerId);
            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.CarTransmissionType, "Id", "Name", car.CarTransmissionTypeId);
            ViewData["CarTypeId"] = new SelectList(_context.CarType, "Id", "Type", car.CarTypeId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", car.ManufacturerId);
            return View(car);
        }
        //[Authorize(Roles = "Admin")]
        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,ManufacturerId,CarTypeId,Engine,BHP,CarTransmissionTypeId,Mileage,Seats,AirBagDetails,BootSpace,Price")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.CarTransmissionType, "Id", "Name", car.CarTransmissionTypeId);
            ViewData["CarTypeId"] = new SelectList(_context.CarType, "Id", "Type", car.CarTypeId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", car.ManufacturerId);
            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.CarTransmissionType)
                .Include(c => c.CarType)
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Car'  is null.");
            }
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
          return (_context.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [Authorize(Roles = "Customer, Admin")]
        public async Task<IActionResult> SearchAll(string columnName,string searchTerm)
        {
            var allIteam = _context.Car.Include(c => c.CarTransmissionType).Include(c => c.CarType).Include(c => c.Manufacturer).AsQueryable();
            if (!String.IsNullOrEmpty(searchTerm))
            {
                switch (columnName.ToLower())
                {
                    case "model":
                        allIteam=allIteam.Where(item=>item.Model.Contains(searchTerm));
                        break;
                    case "price":
                        allIteam = allIteam.Where(item => item.Price.ToString().Contains(searchTerm));
                        break;
                    case "cartype":
                        var carType = await _context.CarType.FirstOrDefaultAsync(ct => ct.Type.Contains(searchTerm));
                        if (carType != null)
                        {
                            allIteam = allIteam.Where(item => item.CarTypeId == carType.Id);
                        }
                        break;
                    case "manufacturer":
                        var manufacturerType = await _context.Manufacturer.FirstOrDefaultAsync(ct => ct.Name.Contains(searchTerm));
                        if(manufacturerType != null)
                        {
                            allIteam=allIteam.Where(item=>item.ManufacturerId == manufacturerType.Id);
                        }
                        break;
                    default:
                        break;
                }
            }
            
            var output=await allIteam.ToListAsync();
            return View("SearchAll", output);
        }
    }
}
