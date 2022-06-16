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
    public class Vaccine_RecordController : Controller
    {
        private readonly Covid_19_WebAppContext _context;

        public Vaccine_RecordController(Covid_19_WebAppContext context)
        {
            _context = context;
        }

        // GET: Vaccine_Record
        public async Task<IActionResult> Index()
        {
              return _context.Vaccine_Record != null ? 
                          View(await _context.Vaccine_Record.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebAppContext.Vaccine_Record'  is null.");
        }

        // GET: Vaccine_Record/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccine_Record == null)
            {
                return NotFound();
            }

            var vaccine_Record = await _context.Vaccine_Record
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (vaccine_Record == null)
            {
                return NotFound();
            }

            return View(vaccine_Record);
        }

        // GET: Vaccine_Record/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaccine_Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordID,CitizenID,Number_Injection,FirstDate,FirstVaccine,SecondDate,SecondVaccine,ThirdDate,ThirdVaccine,First_NurseID,Second_NurseID,Third_NurseID")] Vaccine_Record vaccine_Record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccine_Record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaccine_Record);
        }

        // GET: Vaccine_Record/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccine_Record == null)
            {
                return NotFound();
            }

            var vaccine_Record = await _context.Vaccine_Record.FindAsync(id);
            if (vaccine_Record == null)
            {
                return NotFound();
            }
            return View(vaccine_Record);
        }

        // POST: Vaccine_Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordID,CitizenID,Number_Injection,FirstDate,FirstVaccine,SecondDate,SecondVaccine,ThirdDate,ThirdVaccine,First_NurseID,Second_NurseID,Third_NurseID")] Vaccine_Record vaccine_Record)
        {
            if (id != vaccine_Record.RecordID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccine_Record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Vaccine_RecordExists(vaccine_Record.RecordID))
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
            return View(vaccine_Record);
        }

        // GET: Vaccine_Record/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccine_Record == null)
            {
                return NotFound();
            }

            var vaccine_Record = await _context.Vaccine_Record
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (vaccine_Record == null)
            {
                return NotFound();
            }

            return View(vaccine_Record);
        }

        // POST: Vaccine_Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccine_Record == null)
            {
                return Problem("Entity set 'Covid_19_WebAppContext.Vaccine_Record'  is null.");
            }
            var vaccine_Record = await _context.Vaccine_Record.FindAsync(id);
            if (vaccine_Record != null)
            {
                _context.Vaccine_Record.Remove(vaccine_Record);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Vaccine_RecordExists(int id)
        {
          return (_context.Vaccine_Record?.Any(e => e.RecordID == id)).GetValueOrDefault();
        }
    }
}
