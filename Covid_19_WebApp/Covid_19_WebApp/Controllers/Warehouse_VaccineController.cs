using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid_19_WebApp.Data;
using Covid_19_WebApp.Models;

namespace Covid_19_WebApp.Controllers
{
    public class Warehouse_VaccineController : Controller
    {
        private readonly Covid_19_WebAppContext _context;

        public Warehouse_VaccineController(Covid_19_WebAppContext context)
        {
            _context = context;
        }

        // GET: Warehouse_Vaccine
        public async Task<IActionResult> Index()
        {
              return _context.Warehouse_Vaccine != null ? 
                          View(await _context.Warehouse_Vaccine.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebAppContext.Warehouse_Vaccine'  is null.");
        }

        // GET: Warehouse_Vaccine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Warehouse_Vaccine == null)
            {
                return NotFound();
            }

            var warehouse_Vaccine = await _context.Warehouse_Vaccine
                .FirstOrDefaultAsync(m => m.VaccineId == id);
            if (warehouse_Vaccine == null)
            {
                return NotFound();
            }

            return View(warehouse_Vaccine);
        }

        // GET: Warehouse_Vaccine/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouse_Vaccine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccineId,WarehouseID,Stock")] Warehouse_Vaccine warehouse_Vaccine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouse_Vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse_Vaccine);
        }

        // GET: Warehouse_Vaccine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Warehouse_Vaccine == null)
            {
                return NotFound();
            }

            var warehouse_Vaccine = await _context.Warehouse_Vaccine.FindAsync(id);
            if (warehouse_Vaccine == null)
            {
                return NotFound();
            }
            return View(warehouse_Vaccine);
        }

        // POST: Warehouse_Vaccine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccineId,WarehouseID,Stock")] Warehouse_Vaccine warehouse_Vaccine)
        {
            if (id != warehouse_Vaccine.VaccineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouse_Vaccine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Warehouse_VaccineExists(warehouse_Vaccine.VaccineId))
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
            return View(warehouse_Vaccine);
        }

        // GET: Warehouse_Vaccine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Warehouse_Vaccine == null)
            {
                return NotFound();
            }

            var warehouse_Vaccine = await _context.Warehouse_Vaccine
                .FirstOrDefaultAsync(m => m.VaccineId == id);
            if (warehouse_Vaccine == null)
            {
                return NotFound();
            }

            return View(warehouse_Vaccine);
        }

        // POST: Warehouse_Vaccine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Warehouse_Vaccine == null)
            {
                return Problem("Entity set 'Covid_19_WebAppContext.Warehouse_Vaccine'  is null.");
            }
            var warehouse_Vaccine = await _context.Warehouse_Vaccine.FindAsync(id);
            if (warehouse_Vaccine != null)
            {
                _context.Warehouse_Vaccine.Remove(warehouse_Vaccine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Warehouse_VaccineExists(int id)
        {
          return (_context.Warehouse_Vaccine?.Any(e => e.VaccineId == id)).GetValueOrDefault();
        }
    }
}
