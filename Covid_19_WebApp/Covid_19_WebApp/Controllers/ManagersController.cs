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
using System.Data;
using Newtonsoft.Json;

namespace Covid_19_WebApp.Controllers
{
    public class ManagersController : Controller
    {
        private readonly Covid_19_WebAppContext _context;

        public ManagersController(Covid_19_WebAppContext context)
        {
            _context = context;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
              return _context.Manager != null ? 
                          View(await _context.Manager.ToListAsync()) :
                          Problem("Entity set 'Covid_19_WebAppContext.Manager'  is null.");
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Manager == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager
                .FirstOrDefaultAsync(m => m.ManagerID == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagerID,FName,LName,BDate,Address,Phone,Email,Password")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Manager == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManagerID,FName,LName,BDate,Address,Phone,Email,Password")] Manager manager)
        {
            if (id != manager.ManagerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.ManagerID))
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
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Manager == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager
                .FirstOrDefaultAsync(m => m.ManagerID == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Manager == null)
            {
                return Problem("Entity set 'Covid_19_WebAppContext.Manager'  is null.");
            }
            var manager = await _context.Manager.FindAsync(id);
            if (manager != null)
            {
                _context.Manager.Remove(manager);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
          return (_context.Manager?.Any(e => e.ManagerID == id)).GetValueOrDefault();
        }

        public IActionResult ManagerHomePage()
        {
            return View();
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
            connectionstring constring = new connectionstring();
            con.ConnectionString = constring.GetConnectionString();
        }


        [HttpPost]
        public ActionResult Verify(Manager manager)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Manager where Email='" + manager.Email + "' and Password='" + manager.Password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                GlobalVariables.isManagerLogin = true;
                GlobalVariables.manager.FName = dr.GetString(1);
                con.Close();
                //return View("LoginSuccess");
            }
            else
            {
                GlobalVariables.isManagerAccountCorrect = false;
                con.Close();
            }
            return RedirectToAction("ManagerHome", "Home");
        }

        public IActionResult LogOut()
        {
            GlobalVariables.isManagerLogin = false;
            return RedirectToAction("ManagerHome", "Home");
        }

        public void Statistic_Number_Of_Citizen_Each_Month()
        {
            List<int> Month = new List<int>();
            List<int> NumberOfCitizen = new List<int>();
            List<DataPoint> dataPoints = new List<DataPoint>();
            connectionstring constring = new connectionstring();
            DataTable data = new DataTable();
            string query = "select @month as 'Month', COUNT(*) as 'Number of Citizen' from VACCINE_RECORD where MONTH(FirstDate) = @month or MONTH(SecondDate) = @month or MONTH(ThirdDate) = @month";
            using (SqlConnection connection = new SqlConnection(constring.GetConnectionString()))
            {
                for (int i = 1; i <= 12; i++)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@month", i);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //Month.Add(Convert.ToInt32(reader[0]));
                        //NumberOfCitizen.Add(Convert.ToInt32(reader[1]));
                        dataPoints.Add(new DataPoint(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1])));
                    }
                    connection.Close();
                }
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
        }

        public IActionResult GetChart_Statistic_Number_Of_Citizen_Each_Month()
        {
            Statistic_Number_Of_Citizen_Each_Month();
            return View();
        }

        public void Statistic_Vaccine_For_InjectionNumber()
        {
            string[] VaccineOrder = { "FirstVaccine", "SecondVaccine", "ThirdVaccine" };
            List<DataPointForVaccine> dataPoints = new List<DataPointForVaccine>();
            connectionstring constring = new connectionstring();
            DataTable data = new DataTable();
            string query = "select Number_Injection, COUNT(CitizenID) from VACCINE_RECORD group by Number_Injection";
            using (SqlConnection connection = new SqlConnection(constring.GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //Month.Add(Convert.ToInt32(reader[0]));
                    //NumberOfCitizen.Add(Convert.ToInt32(reader[1]));
                    if (Convert.ToInt32(reader[0]) == 1)
                    {
                        dataPoints.Add(new DataPointForVaccine("First Vaccination", Convert.ToInt32(reader[1])));
                    }
                    else if (Convert.ToInt32(reader[0]) == 2)
                    {
                        dataPoints.Add(new DataPointForVaccine("Second Vaccination", Convert.ToInt32(reader[1])));
                    }
                    else
                    {
                        dataPoints.Add(new DataPointForVaccine("Third Vaccination", Convert.ToInt32(reader[1])));
                    }
                    
                }
                connection.Close();


            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
        }

        public IActionResult GetChart_Statistic_Vaccine_For_InjectionNumber()
        {
            Statistic_Vaccine_For_InjectionNumber();
            return View();
        }


    }
}
