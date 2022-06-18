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
                //_context.Add(citizen);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                _context.Add(citizen);
                try
                {
                    await _context.SaveChangesAsync();
                    GlobalVariables.isRegisterSuccessfully = true;
                    ViewBag.Message = "Register Successfully! You Can Login Into The Website Right Now.";
                    return RedirectToAction("Login", "Citizens");
                }
                catch
                {
                    ViewBag.Message = "Citizens must be older than or equal to 18 years old.";
                    //return View("InsertTrigger");
                }
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
                return RedirectToAction(nameof(CitizenInformation));
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
                GlobalVariables.citizen.CitizenId = dr.GetInt32(0);
                GlobalVariables.citizen.FName = dr.GetString(1);
                GlobalVariables.citizen.LName = dr.GetString(2);
                GlobalVariables.citizen.Bdate = dr.GetDateTime(3);
                GlobalVariables.citizen.Address = dr.GetString(4);
                GlobalVariables.citizen.Phone = dr.GetString(6);
                GlobalVariables.citizen.Gender = dr.GetString(8);
                GlobalVariables.citizen.Email = dr.GetString(7);


                GlobalVariables.day = GlobalVariables.citizen.Bdate.Day;
                GlobalVariables.month = GlobalVariables.citizen.Bdate.Month;
                GlobalVariables.year = GlobalVariables.citizen.Bdate.Year;


                string day_str = "";
                string month_str = "";

                if (GlobalVariables.day <= 9)
                {
                    day_str = "0" + Convert.ToString(GlobalVariables.day);
                }
                else if (GlobalVariables.day > 9)
                {
                    day_str =  Convert.ToString(GlobalVariables.day);
                }

                if (GlobalVariables.month <= 9)
                {
                    month_str = "0" + Convert.ToString(GlobalVariables.month);
                }
                else if (GlobalVariables.month > 9)
                {
                    month_str = Convert.ToString(GlobalVariables.month);
                }

                GlobalVariables.DateOfBirth = Convert.ToString(GlobalVariables.year) + "-" + month_str + "-" + day_str;
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
            GlobalVariables.vaccineRecord.FirstDate=null;
            GlobalVariables.vaccineRecord.FirstVaccine = null;
            GlobalVariables.vaccineRecord.SecondDate = null;
            GlobalVariables.vaccineRecord.SecondVaccine = null;
            GlobalVariables.vaccineRecord.ThirdDate = null;
            GlobalVariables.vaccineRecord.ThirdVaccine = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CitizenInformation(Citizen citizen)
        {
            List<Citizen> foundCitizen = new List<Citizen>();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Citizen where CitizenID = '" + GlobalVariables.citizen.CitizenId + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                foundCitizen.Add(new Citizen { CitizenId = (int)dr.GetInt32(0), FName = dr.GetString(1), LName = dr.GetString(2),
                    Bdate = dr.GetDateTime(3), Address = dr.GetString(4), Phone = dr.GetString(6), Gender = dr.GetString(8), Email = dr.GetString(7) });
                con.Close();
                return View("CitizenInformation", foundCitizen);
            }
            else
            {
                GlobalVariables.isCorrect = false;
                con.Close();
                return View("Login");
            }
        }

        public IActionResult SearchCitzen(string SearchTerm)
        {
            List<Citizen> foundCitizen = new List<Citizen>();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Citizen where FName like " + "'%" + SearchTerm + "%'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                foundCitizen.Add(new Citizen
                {
                    CitizenId = (int)dr.GetInt32(0),
                    FName = dr.GetString(1),
                    LName = dr.GetString(2),
                    Bdate = dr.GetDateTime(3),
                    Address = dr.GetString(4),
                    Phone = dr.GetString(6),
                    Gender = dr.GetString(8),
                    Email = dr.GetString(7)
                });
                con.Close();
                return View("Index", foundCitizen);
            }
            else
            {
                return RedirectToAction("Index", "Citizens", "Not Found");
            }
            //return RedirectToAction("ManagerHome", "Home");
        }

        public IActionResult VaccinationHistory()
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Vaccine_Record where CitizenID = '" + GlobalVariables.citizen.CitizenId + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (dr.IsDBNull(3) == false)
                {
                    GlobalVariables.vaccineRecord.FirstDate = dr.GetDateTime(3);
                }
                else
                {
                    GlobalVariables.vaccineRecord.FirstDate = null;
                }

                if(dr.IsDBNull(4) == false)
                {
                    GlobalVariables.vaccineRecord.FirstVaccine = dr.GetString(4);
                }
                else
                {
                    GlobalVariables.vaccineRecord.FirstDate = null;
                }

                if(dr.IsDBNull(5) == false)
                {
                    GlobalVariables.vaccineRecord.SecondDate = dr.GetDateTime(5);
                }
                else
                {
                    GlobalVariables.vaccineRecord.SecondDate = null;
                }

                if (dr.IsDBNull(6) == false)
                {
                    GlobalVariables.vaccineRecord.SecondVaccine = dr.GetString(6);
                }
                else
                {
                    GlobalVariables.vaccineRecord.SecondVaccine = null;
                }

                if(dr.IsDBNull(7) == false)
                {
                    GlobalVariables.vaccineRecord.ThirdDate = dr.GetDateTime(7);
                }
                else
                {
                    GlobalVariables.vaccineRecord.ThirdDate = null;
                }

                if(dr.IsDBNull(8) == false)
                {
                    GlobalVariables.vaccineRecord.ThirdVaccine = dr.GetString(8);
                }
                else
                {
                    GlobalVariables.vaccineRecord.ThirdVaccine = null;
                }

            }

            return View();
        }

        public IActionResult UserInformation()
        {
            return View();
        }
    }
}
