using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.Data;
using HRApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HRApplication.Controllers
{
    public class EmployeeController : Controller
    {
        public IConfiguration Configuration;
        public AppDbContext AppDbContext { get; set; }

        public EmployeeController(IConfiguration configuration, AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Configuration = configuration;
        }
        // GET: Employee
        public ActionResult Index()
        {
            var contract = from e in AppDbContext.Employee where e.Status.Contains("Contract") select e;
            var permanent = from e in AppDbContext.Employee where e.Status.Contains("Permanent") select e;
            var probation = from e in AppDbContext.Employee where e.Status.Contains("Probition") select e;
            ViewBag.Contract = contract;
            ViewBag.Permanent = permanent;
            ViewBag.Probation = probation;
            return View("Employee");
        }

        // GET: Employee
        public ActionResult Add()
        {
            return View("AddEmployee");
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(string Fullname,string Email,string Phone,string Status,int Gender,DateTime Birthdate,string Birthplace,string Address,string Position,string Division,string emername,string emerphone, [FromForm(Name = "Photo")]IFormFile Photo)
        {
            try
            {
                Console.WriteLine("Masuk sini");
                List<string> emergency = new List<string>();
                emergency.Add(emername +","+ emerphone);
                Console.WriteLine();
                if (!System.IO.Directory.Exists("wwwroot" + "/Image/"))
                {
                    System.IO.Directory.CreateDirectory("wwwroot" + "/Image/");
                }
                string storePath = "wwwroot/Image/";
                if (Photo != null)
                {
                    Console.WriteLine("Masuk sini juga");
                    var path = Path.Combine(storePath, Photo.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Photo.CopyTo(stream);
                    }
                    var pathfix = path.Substring(8);
                    Employe employee = new Employe()
                    {
                        Fullname = Fullname,
                        Email = Email,
                        Phone_number = Phone,
                        Address = Address,
                        Birthdate = Birthdate,
                        Birthplace = Birthplace,
                        Gender = Gender,
                        Position = Position,
                        Division = Division,
                        Status = Status,
                        Emergencycontact = emergency,
                        Photo = pathfix,
                        Created_at = DateTime.Now,
                    };
                    AppDbContext.Employee.Add(employee);
                }
                AppDbContext.SaveChanges();
                return RedirectToAction("Index","Employee");
            }
            catch
            {
                return View();
            }
        }
        // POST: Employee/Create
        [HttpPost]
        public ActionResult Add(string Fullname, string Email, string Phone, string Status, int Gender, DateTime Birthdate, string Birthplace, string Address, string Position, string Division, string emername, string emerphone, [FromForm(Name = "Photo")]IFormFile Photo)
        {
            try
            {
                Console.WriteLine("Masuk sini");
                List<string> emergency = new List<string>();
                emergency.Add(emername + "," + emerphone);
                Console.WriteLine();
                if (!System.IO.Directory.Exists("wwwroot" + "/Image/"))
                {
                    System.IO.Directory.CreateDirectory("wwwroot" + "/Image/");
                }
                string storePath = "wwwroot/Image/";
                if (Photo != null)
                {
                    Console.WriteLine("Masuk sini juga");
                    var path = Path.Combine(storePath, Photo.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Photo.CopyTo(stream);
                    }
                    var pathfix = path.Substring(8);
                    Employe employee = new Employe()
                    {
                        Fullname = Fullname,
                        Email = Email,
                        Phone_number = Phone,
                        Address = Address,
                        Birthdate = Birthdate,
                        Birthplace = Birthplace,
                        Gender = Gender,
                        Position = Position,
                        Division = Division,
                        Status = Status,
                        Emergencycontact = emergency,
                        Photo = pathfix,
                        Created_at = DateTime.Now,
                    };
                    AppDbContext.Employee.Add(employee);
                }
                AppDbContext.SaveChanges();
                return RedirectToAction("Add", "Employee");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}