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
    public class Entry_DetailController : Controller
    {
        private readonly Covid_19_WebAppContext _context;

        public Entry_DetailController(Covid_19_WebAppContext context)
        {
            _context = context;
        }

        // GET: Entry_Detail
        public async Task<IActionResult> Index()
        {
              return _context.Entry_Detail != null ? 
                          View(await _context.Entry_Detail.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebAppContext.Entry_Detail'  is null.");
        }

        // GET: Entry_Detail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entry_Detail == null)
            {
                return NotFound();
            }

            var entry_Detail = await _context.Entry_Detail
                .FirstOrDefaultAsync(m => m.VaccineID == id);
            if (entry_Detail == null)
            {
                return NotFound();
            }

            return View(entry_Detail);
        }

        // GET: Entry_Detail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entry_Detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccineID,EntryID,Quantity")] Entry_Detail entry_Detail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entry_Detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entry_Detail);
        }

        // GET: Entry_Detail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entry_Detail == null)
            {
                return NotFound();
            }

            var entry_Detail = await _context.Entry_Detail.FindAsync(id);
            if (entry_Detail == null)
            {
                return NotFound();
            }
            return View(entry_Detail);
        }

        // POST: Entry_Detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccineID,EntryID,Quantity")] Entry_Detail entry_Detail)
        {
            if (id != entry_Detail.VaccineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entry_Detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Entry_DetailExists(entry_Detail.VaccineID))
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
            return View(entry_Detail);
        }

        // GET: Entry_Detail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entry_Detail == null)
            {
                return NotFound();
            }

            var entry_Detail = await _context.Entry_Detail
                .FirstOrDefaultAsync(m => m.VaccineID == id);
            if (entry_Detail == null)
            {
                return NotFound();
            }

            return View(entry_Detail);
        }

        // POST: Entry_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entry_Detail == null)
            {
                return Problem("Entity set 'Covid_19_WebAppContext.Entry_Detail'  is null.");
            }
            var entry_Detail = await _context.Entry_Detail.FindAsync(id);
            if (entry_Detail != null)
            {
                _context.Entry_Detail.Remove(entry_Detail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Entry_DetailExists(int id)
        {
          return (_context.Entry_Detail?.Any(e => e.VaccineID == id)).GetValueOrDefault();
        }
    }
}
