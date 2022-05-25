using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid_19_WebApp_Project.Data;
using Covid_19_WebApp_Project.Models;

namespace Covid_19_WebApp_Project.Controllers
{
    public class CitizenModelsController : Controller
    {
        private readonly Covid_19_WebApp_ProjectContext _context;

        public CitizenModelsController(Covid_19_WebApp_ProjectContext context)
        {
            _context = context;
        }

        // GET: CitizenModels
        public async Task<IActionResult> Index()
        {
              return _context.CitizenModel != null ? 
                          View(await _context.CitizenModel.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebApp_ProjectContext.CitizenModel'  is null.");
        }

        // GET: CitizenModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CitizenModel == null)
            {
                return NotFound();
            }

            var citizenModel = await _context.CitizenModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (citizenModel == null)
            {
                return NotFound();
            }

            return View(citizenModel);
        }

        // GET: CitizenModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CitizenModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FName,LName,Bdate,Address,Age,Password,Phone,Gender")] CitizenModel citizenModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citizenModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(citizenModel);
        }

        // GET: CitizenModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CitizenModel == null)
            {
                return NotFound();
            }

            var citizenModel = await _context.CitizenModel.FindAsync(id);
            if (citizenModel == null)
            {
                return NotFound();
            }
            return View(citizenModel);
        }

        // POST: CitizenModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FName,LName,Bdate,Address,Age,Password,Phone,Gender")] CitizenModel citizenModel)
        {
            if (id != citizenModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citizenModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitizenModelExists(citizenModel.Id))
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
            return View(citizenModel);
        }

        // GET: CitizenModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CitizenModel == null)
            {
                return NotFound();
            }

            var citizenModel = await _context.CitizenModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (citizenModel == null)
            {
                return NotFound();
            }

            return View(citizenModel);
        }

        // POST: CitizenModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CitizenModel == null)
            {
                return Problem("Entity set 'Covid_19_WebApp_ProjectContext.CitizenModel'  is null.");
            }
            var citizenModel = await _context.CitizenModel.FindAsync(id);
            if (citizenModel != null)
            {
                _context.CitizenModel.Remove(citizenModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitizenModelExists(int id)
        {
          return (_context.CitizenModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
