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
    public class WareHousesController : Controller
    {
        private readonly Covid_19_WebAppContext _context;

        public WareHousesController(Covid_19_WebAppContext context)
        {
            _context = context;
        }

        // GET: WareHouses
        public async Task<IActionResult> Index()
        {
              return _context.WareHouse != null ? 
                          View(await _context.WareHouse.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebAppContext.WareHouse'  is null.");
        }

        // GET: WareHouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WareHouse == null)
            {
                return NotFound();
            }

            var wareHouse = await _context.WareHouse
                .FirstOrDefaultAsync(m => m.WarehouseID == id);
            if (wareHouse == null)
            {
                return NotFound();
            }

            return View(wareHouse);
        }

        // GET: WareHouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WareHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WarehouseID,Location")] WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wareHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wareHouse);
        }

        // GET: WareHouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WareHouse == null)
            {
                return NotFound();
            }

            var wareHouse = await _context.WareHouse.FindAsync(id);
            if (wareHouse == null)
            {
                return NotFound();
            }
            return View(wareHouse);
        }

        // POST: WareHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseID,Location")] WareHouse wareHouse)
        {
            if (id != wareHouse.WarehouseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wareHouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WareHouseExists(wareHouse.WarehouseID))
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
            return View(wareHouse);
        }

        // GET: WareHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WareHouse == null)
            {
                return NotFound();
            }

            var wareHouse = await _context.WareHouse
                .FirstOrDefaultAsync(m => m.WarehouseID == id);
            if (wareHouse == null)
            {
                return NotFound();
            }

            return View(wareHouse);
        }

        // POST: WareHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WareHouse == null)
            {
                return Problem("Entity set 'Covid_19_WebAppContext.WareHouse'  is null.");
            }
            var wareHouse = await _context.WareHouse.FindAsync(id);
            if (wareHouse != null)
            {
                _context.WareHouse.Remove(wareHouse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WareHouseExists(int id)
        {
          return (_context.WareHouse?.Any(e => e.WarehouseID == id)).GetValueOrDefault();
        }
    }
}
