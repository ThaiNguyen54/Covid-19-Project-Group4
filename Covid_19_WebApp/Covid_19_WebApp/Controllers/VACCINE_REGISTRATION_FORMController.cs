using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid_19_WebApp.Data;
using Covid_19_WebApp.Models;
using Microsoft.Data.SqlClient;

namespace Covid_19_WebApp.Controllers
{
    public class VACCINE_REGISTRATION_FORMController : Controller
    {
        private readonly Covid_19_WebAppContext _context;

        public VACCINE_REGISTRATION_FORMController(Covid_19_WebAppContext context)
        {
            _context = context;
        }

        // GET: VACCINE_REGISTRATION_FORM
        public async Task<IActionResult> Index()
        {
              return _context.VACCINE_REGISTRATION_FORM != null ? 
                          View(await _context.VACCINE_REGISTRATION_FORM.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebAppContext.RegistrationForm'  is null.");
        }

        // GET: VACCINE_REGISTRATION_FORM/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VACCINE_REGISTRATION_FORM == null)
            {
                return NotFound();
            }

            var vACCINE_REGISTRATION_FORM = await _context.VACCINE_REGISTRATION_FORM
                .FirstOrDefaultAsync(m => m.FormID == id);
            if (vACCINE_REGISTRATION_FORM == null)
            {
                return NotFound();
            }

            return View(vACCINE_REGISTRATION_FORM);
        }

        // GET: VACCINE_REGISTRATION_FORM/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VACCINE_REGISTRATION_FORM/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForm([Bind("CitizenID, VaccineID, InjectionDate, RegisterDate, InjectionNumber")] VACCINE_REGISTRATION_FORM vACCINE_REGISTRATION_FORM)
        {
            int count = 0;
            vACCINE_REGISTRATION_FORM.RegisterDate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(vACCINE_REGISTRATION_FORM);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException ex)
                {
                    string[] remove = { "The transaction ended in the trigger. The batch has been aborted.", "The statement has been terminated.", "The ROLLBACK TRANSACTION request has no corresponding BEGIN TRANSACTION.", "\n", "\r"};
                    ViewBag.Message = Convert.ToString(ex.InnerException.Message);
                    GlobalVariables.error = true;
                    foreach (string item in remove)
                    {
                        if (ViewBag.Message.Contains(item))
                        {
                            //ViewBag.Message = ViewBag.Message.Substring(0, ViewBag.Message.LastIndexOf(item));
                            
                            if(item == "\r")
                            {
                                ViewBag.Message = ViewBag.Message.Replace(item, ". ");
                            }
                            else
                            {
                                ViewBag.Message = ViewBag.Message.Replace(item, "");
                            }
                        }
                    }
                    return View("Create");
                }
                
            }
            return View(vACCINE_REGISTRATION_FORM);
        }

        // GET: VACCINE_REGISTRATION_FORM/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VACCINE_REGISTRATION_FORM == null)
            {
                return NotFound();
            }

            var vACCINE_REGISTRATION_FORM = await _context.VACCINE_REGISTRATION_FORM.FindAsync(id);
            if (vACCINE_REGISTRATION_FORM == null)
            {
                return NotFound();
            }
            return View(vACCINE_REGISTRATION_FORM);
        }

        // POST: VACCINE_REGISTRATION_FORM/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormID,VaccineID,CitizenID,InjectionDate,RegisterDate,InjectionNumber")] VACCINE_REGISTRATION_FORM vACCINE_REGISTRATION_FORM)
        {
            if (id != vACCINE_REGISTRATION_FORM.FormID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vACCINE_REGISTRATION_FORM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VACCINE_REGISTRATION_FORMExists(vACCINE_REGISTRATION_FORM.FormID))
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
            return View(vACCINE_REGISTRATION_FORM);
        }

        // GET: VACCINE_REGISTRATION_FORM/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VACCINE_REGISTRATION_FORM == null)
            {
                return NotFound();
            }

            var vACCINE_REGISTRATION_FORM = await _context.VACCINE_REGISTRATION_FORM
                .FirstOrDefaultAsync(m => m.FormID == id);
            if (vACCINE_REGISTRATION_FORM == null)
            {
                return NotFound();
            }

            return View(vACCINE_REGISTRATION_FORM);
        }

        // POST: VACCINE_REGISTRATION_FORM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VACCINE_REGISTRATION_FORM == null)
            {
                return Problem("Entity set 'Covid_19_WebAppContext.RegistrationForm'  is null.");
            }
            var vACCINE_REGISTRATION_FORM = await _context.VACCINE_REGISTRATION_FORM.FindAsync(id);
            if (vACCINE_REGISTRATION_FORM != null)
            {
                _context.VACCINE_REGISTRATION_FORM.Remove(vACCINE_REGISTRATION_FORM);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VACCINE_REGISTRATION_FORMExists(int id)
        {
          return (_context.VACCINE_REGISTRATION_FORM?.Any(e => e.FormID == id)).GetValueOrDefault();
        }



    }
}
