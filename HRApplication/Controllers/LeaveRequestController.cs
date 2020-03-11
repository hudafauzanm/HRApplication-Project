using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRApplication.Data;
using HRApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HRApplication.Controllers
{
    public class LeaveRequestController : Controller
    {
        public IConfiguration Configuration;
        public AppDbContext AppDbContext { get; set; }

        public LeaveRequestController(IConfiguration configuration, AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Configuration = configuration;
        }
        // GET: LeaveRequest
        public ActionResult Index()
        {
            List<IQueryable> userappr = new List<IQueryable>();
            List<IQueryable> userpend = new List<IQueryable>();
            List<IQueryable> userreject = new List<IQueryable>();
            var approved = from e in AppDbContext.LeaveRequest  where e.Status.Contains("approve") select e;
            var pending = from e in AppDbContext.LeaveRequest  where e.Status.Contains("pending") select e;
            var rejected = from e in AppDbContext.LeaveRequest  where e.Status.Contains("reject") select e;
            
            foreach(var pen in pending)
            {
               var hasil = from x in AppDbContext.Employee where x.Id == Guid.Parse(pen.EmployeeId) select x;
               userpend.Add(hasil);
            }
            foreach (var pen in approved)
            {
                Console.WriteLine(pen.EmployeeId);
                var hasil = from x in AppDbContext.Employee where x.Id == Guid.Parse(pen.EmployeeId) select x;
                userappr.Add(hasil);
            }
            foreach (var pen in rejected)
            {
                var hasil = from x in AppDbContext.Employee where x.Id == Guid.Parse(pen.EmployeeId) select x;
                userreject.Add(hasil);
            }
            var emp = from e in AppDbContext.Employee select e;
            var lid = from e in AppDbContext.LeaveRequest where e.Status.Contains("pending") select e.Id;
            var notif = (from e in AppDbContext.LeaveRequest where e.Read_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e).Count();
            ViewBag.Notif = notif;
            ViewBag.Lid = lid;
            ViewBag.Emp = emp;
            ViewBag.Pend = userpend;
            ViewBag.Appr = userappr;
            ViewBag.Reject = userreject;
            return View("LeaveRequest");
        }

        // GET: LeaveRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public FileResult ExportAll()
        {
            var export_leave = from ex in AppDbContext.LeaveRequest select ex;
            string[] colomnama = new string[5] {"Employee ID", "FullName", "Status", "LeaveTime", "Created_At" };
            string csv = string.Empty;
        
            foreach (string columnName in colomnama)
            {
                csv += columnName + ',';
            }
            csv += "\r\n";
            
            foreach (var appl in export_leave)
            {
                //Add the Data rows.
                csv += appl.EmployeeId.Replace(",", ";") + ',';
                csv += appl.FullName.Replace(",", ";") + ',';
                csv += appl.Status.Replace(",", ";") + ',';
                csv += appl.LeaveTime;
                csv += ",";
                csv += appl.Created_at;
                csv += ",";
   
                //Add new line.
                csv += "\r\n";
            }
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "LeaveRequestAll.csv");
        }

        public FileResult Export(string status,string search)
        {
            Console.WriteLine(status);
            var export_leave = from ex in AppDbContext.LeaveRequest where ex.Status.Contains(status) || ex.FullName.Contains(search) select ex;
            string[] colomnama = new string[5] { "Employee ID", "FullName", "Status", "LeaveTime", "Created_At" };
            string csv = string.Empty;

            foreach (string columnName in colomnama)
            {
                csv += columnName + ',';
            }
            csv += "\r\n";

            foreach (var appl in export_leave)
            {
                //Add the Data rows.
                csv += appl.EmployeeId.Replace(",", ";") + ',';
                csv += appl.FullName.Replace(",", ";") + ',';
                csv += appl.Status.Replace(",", ";") + ',';
                csv += appl.LeaveTime;
                csv += ",";
                csv += appl.Created_at;
                csv += ",";

                //Add new line.
                csv += "\r\n";
            }
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", $"LeaveRequest{status}{search}.csv");
        }
        // GET: LeaveRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        public ActionResult Create(string Employee,DateTime time)
        {
            try
            {
                string fullname = string.Empty;
                var empname = from x in AppDbContext.Employee where x.Id == Guid.Parse(Employee) select x;
                foreach(var x in empname)
                {
                    fullname = x.Fullname;
                }
                Console.WriteLine("masuk sini dong");
                LeaveRequest leave = new LeaveRequest()
                {
                    EmployeeId = Employee,
                    FullName = fullname,
                    Status = "pending",
                    LeaveTime = time,
                    Created_at = DateTime.Now
                };
                AppDbContext.LeaveRequest.Add(leave);
                AppDbContext.SaveChanges();

                return RedirectToAction("Index","LeaveRequest");
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Edit/5
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

        // GET: LeaveRequest/Delete/5
        public ActionResult Approve(string id)
        {
            
            var leave = from l in AppDbContext.LeaveRequest where l.EmployeeId == id select l;
            string idleave = "";
            foreach(var x in leave)
            {
                idleave = Convert.ToString(x.Id);
            }
            Console.WriteLine(idleave);
            var idedits = Guid.Parse(idleave);
            var emp = AppDbContext.LeaveRequest.Find(idedits);
            emp.Status = "approve";
            emp.Read_at = DateTime.Now;
            AppDbContext.SaveChanges();
            return RedirectToAction("Index","LeaveRequest");
        }
        public ActionResult Reject(string id)
        {
            var leave = from l in AppDbContext.LeaveRequest where l.EmployeeId == id select l;
            string idleave = "";
            foreach (var x in leave)
            {
                Console.WriteLine(x.Id);
                idleave = Convert.ToString(x.Id);
            }
            var idedits = Guid.Parse(idleave);
            var emp = AppDbContext.LeaveRequest.Find(idedits);
            emp.Status = "reject";
            emp.Read_at = DateTime.Now;
            AppDbContext.SaveChanges();
            return RedirectToAction("Index", "LeaveRequest");
        }

        // POST: LeaveRequest/Delete/5
        [HttpPost]
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

        public ActionResult UpdateNotif()
        {
            var notif = from r in AppDbContext.LeaveRequest select r;
            foreach (var recv in notif)
            {
                var find = AppDbContext.LeaveRequest.Find(recv.Id);
                find.Read_at = DateTime.Now;
            }
            AppDbContext.SaveChanges();
            return Ok("success");
        }
    }
}