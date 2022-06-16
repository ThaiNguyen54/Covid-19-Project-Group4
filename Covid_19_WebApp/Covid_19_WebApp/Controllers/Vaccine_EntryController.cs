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
    public class Vaccine_EntryController : Controller
    {
        private readonly Covid_19_WebAppContext _context;

        public Vaccine_EntryController(Covid_19_WebAppContext context)
        {
            _context = context;
        }

        // GET: Vaccine_Entry
        public async Task<IActionResult> Index()
        {
              return _context.Vaccine_Entry != null ? 
                          View(await _context.Vaccine_Entry.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebAppContext.Vaccine_Entry'  is null.");
        }

        // GET: Vaccine_Entry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccine_Entry == null)
            {
                return NotFound();
            }

            var vaccine_Entry = await _context.Vaccine_Entry
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (vaccine_Entry == null)
            {
                return NotFound();
            }

            return View(vaccine_Entry);
        }

        // GET: Vaccine_Entry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaccine_Entry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryId,ManagerID,SupID,EntryDate,Amount_Vaccine")] Vaccine_Entry vaccine_Entry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccine_Entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaccine_Entry);
        }

        // GET: Vaccine_Entry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccine_Entry == null)
            {
                return NotFound();
            }

            var vaccine_Entry = await _context.Vaccine_Entry.FindAsync(id);
            if (vaccine_Entry == null)
            {
                return NotFound();
            }
            return View(vaccine_Entry);
        }

        // POST: Vaccine_Entry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntryId,ManagerID,SupID,EntryDate,Amount_Vaccine")] Vaccine_Entry vaccine_Entry)
        {
            if (id != vaccine_Entry.EntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccine_Entry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Vaccine_EntryExists(vaccine_Entry.EntryId))
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
            return View(vaccine_Entry);
        }

        // GET: Vaccine_Entry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccine_Entry == null)
            {
                return NotFound();
            }

            var vaccine_Entry = await _context.Vaccine_Entry
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (vaccine_Entry == null)
            {
                return NotFound();
            }

            return View(vaccine_Entry);
        }

        // POST: Vaccine_Entry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccine_Entry == null)
            {
                return Problem("Entity set 'Covid_19_WebAppContext.Vaccine_Entry'  is null.");
            }
            var vaccine_Entry = await _context.Vaccine_Entry.FindAsync(id);
            if (vaccine_Entry != null)
            {
                _context.Vaccine_Entry.Remove(vaccine_Entry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Vaccine_EntryExists(int id)
        {
          return (_context.Vaccine_Entry?.Any(e => e.EntryId == id)).GetValueOrDefault();
        }
    }
}
