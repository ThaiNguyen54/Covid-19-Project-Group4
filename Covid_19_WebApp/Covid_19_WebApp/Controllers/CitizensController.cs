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
    public class CitizensController : Controller
    {
        private readonly Covid_19_WebAppContext _context;

        public CitizensController(Covid_19_WebAppContext context)
        {
            _context = context;
        }

        // GET: Citizens
        public async Task<IActionResult> Index()
        {
              return _context.Citizen != null ? 
                          View(await _context.Citizen.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebAppContext.Citizen'  is null.");
        }

        // GET: Citizens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Citizen == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen
                .FirstOrDefaultAsync(m => m.CitizenId == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // GET: Citizens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitizenId,FName,LName,Bdate,Address,Age,Password,Phone,Gender,Email")] Citizen citizen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citizen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(citizen);
        }

        // GET: Citizens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Citizen == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }
            return View(citizen);
        }

        // POST: Citizens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitizenId,FName,LName,Bdate,Address,Age,Password,Phone,Gender,Email")] Citizen citizen)
        {
            if (id != citizen.CitizenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citizen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitizenExists(citizen.CitizenId))
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
            return View(citizen);
        }

        // GET: Citizens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Citizen == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen
                .FirstOrDefaultAsync(m => m.CitizenId == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // POST: Citizens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Citizen == null)
            {
                return Problem("Entity set 'Covid_19_WebAppContext.Citizen'  is null.");
            }
            var citizen = await _context.Citizen.FindAsync(id);
            if (citizen != null)
            {
                _context.Citizen.Remove(citizen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitizenExists(int id)
        {
          return (_context.Citizen?.Any(e => e.CitizenId == id)).GetValueOrDefault();
        }



        // Login
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        // GET: Citizen
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        void connectionString()
        {
            connectionstring connectionString = new connectionstring();
            con.ConnectionString = connectionString.GetConnectionString();
        }
       

        [HttpPost]
        public ActionResult Verify(Citizen citizen)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Citizen where Email='" + citizen.Email + "' and Password='" + citizen.Password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                GlobalVariables.isLogin = true;
                GlobalVariables.citizen.FName = dr.GetString(1);
                con.Close();
                //return View("LoginSuccess");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                GlobalVariables.isCorrect = false;
                con.Close();
                return View("Login");
            }
        }

        public IActionResult LogOut()
        {
            GlobalVariables.isLogin = false;
            return RedirectToAction("Index", "Home");
        }
    }
}
