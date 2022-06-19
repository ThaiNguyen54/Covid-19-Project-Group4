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
    public class NursesController : Controller
    {
        private readonly Covid_19_WebAppContext _context;

        public NursesController(Covid_19_WebAppContext context)
        {
            _context = context;
        }

        // GET: Nurses
        public async Task<IActionResult> Index()
        {
              return _context.Nurse != null ? 
                          View(await _context.Nurse.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebAppContext.Nurse'  is null.");
        }

        // GET: Nurses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nurse == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse
                .FirstOrDefaultAsync(m => m.NurseID == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // GET: Nurses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nurses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NurseID,ManagerID,FName,LName,Bdate,Address,Password,Phone,Gender,Email")] Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nurse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nurse);
        }

        // GET: Nurses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nurse == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            return View(nurse);
        }

        // POST: Nurses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NurseID,ManagerID,FName,LName,Bdate,Address,Password,Phone,Gender,Email")] Nurse nurse)
        {
            if (id != nurse.NurseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nurse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NurseExists(nurse.NurseID))
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
            return View(nurse);
        }

        // GET: Nurses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nurse == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse
                .FirstOrDefaultAsync(m => m.NurseID == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // POST: Nurses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nurse == null)
            {
                return Problem("Entity set 'Covid_19_WebAppContext.Nurse'  is null.");
            }
            var nurse = await _context.Nurse.FindAsync(id);
            if (nurse != null)
            {
                _context.Nurse.Remove(nurse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NurseExists(int id)
        {
          return (_context.Nurse?.Any(e => e.NurseID == id)).GetValueOrDefault();
        }

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        void connectionString()
        {
            connectionstring connectionString = new connectionstring();
            con.ConnectionString = connectionString.GetConnectionString();
        }

        public IActionResult SearchNurse(string NurseName)
        {
            List<Nurse> foundNurse = new List<Nurse>();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Nurse where FName like " + "'%" + NurseName + "%'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                foundNurse.Add(new Nurse
                {
                    NurseID = (int)dr.GetInt32(0),
                    ManagerID = dr.GetInt32(1),
                    FName = dr.GetString(2),
                    LName = dr.GetString(3),
                    Bdate = dr.GetDateTime(4),
                    Address = dr.GetString(5),
                    Phone = dr.GetString(6),
                    Email = dr.GetString(8),
                    Gender = dr.GetString(9)
                });
                con.Close();
                return View("Index", foundNurse);
            }
            else
            {
                GlobalVariables.NotFound = true;
                return RedirectToAction("Index", "Nurses", "Not Found");
            }
            //return RedirectToAction("ManagerHome", "Home");
        }
    }
}
