using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.Data;
using HRApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HRApplication.Controllers
{

    public class AttendanceController : Controller
    {
        public IConfiguration Configuration;
        public AppDbContext AppDbContext { get; set; }

        public AttendanceController(IConfiguration configuration, AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Configuration = configuration;
        }
        // GET: Attandance
        public ActionResult Index()
        {
            var emp = from e in AppDbContext.Employee where e.Resign_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e;
            List<IQueryable> empatt = new List<IQueryable>();
            foreach(var e in emp)
            {
                var attendance = from att in AppDbContext.Attendance where att.EmployeeId == Convert.ToString(e.Id) && att.Clockin.Date == DateTime.Today.Date select att;
                empatt.Add(attendance);
            }
            var notif = (from e in AppDbContext.LeaveRequest where e.Read_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e).Count();
            ViewBag.Notif = notif;
            ViewBag.Emp = emp;
            ViewBag.Att = empatt;

            return View("Attendance");
        }

        public ActionResult Attendance(string Id)
        { 
            var att = from a in AppDbContext.Attendance where a.EmployeeId == Id select a;
            ViewBag.Date = att;
            return View("EmployeeAttendance");
        }

        // GET: Attandance/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Attandance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attandance/Create
        [HttpPost]
        public ActionResult Clockin(string EmployeeId, DateTime clockin, DateTime clockout)
        {
            try
            {
                
                var attendances = from x in AppDbContext.Attendance where x.EmployeeId == EmployeeId && x.Clockin.Date == clockout.Date select x;
                string id = "";
                foreach (var d in attendances)
                {
                    id = Convert.ToString(d.Id);
                }
                if (attendances.Count() == 0)
                {
                    if(clockin.TimeOfDay > TimeSpan.Parse("07:00:00"))
                    {
                        Attendance attendance = new Attendance()
                        {
                            EmployeeId = EmployeeId,
                            Clockin = clockin,
                            Status = 4,
                            Created_at = DateTime.Now
                        };
                        AppDbContext.Attendance.Add(attendance);
                    }
                    else
                    {
                        Attendance attendance = new Attendance()
                        {
                            EmployeeId = EmployeeId,
                            Clockin = clockin,
                            Status = 1,
                            Created_at = DateTime.Now
                        };
                        AppDbContext.Attendance.Add(attendance);
                    }
                    AppDbContext.SaveChanges();
                    return RedirectToAction("Index", "Attendance");
                }
                else
                {
                    Console.WriteLine(clockout.TimeOfDay);
                    var idedit = Guid.Parse(id);
                    if (clockout.TimeOfDay > TimeSpan.Parse("19:00:00") )
                    {
                        var attendanced = AppDbContext.Attendance.Find(idedit);
                        attendanced.Status = 3;
                        attendanced.Clockout = clockout;
                        AppDbContext.SaveChanges();
                    }
                    else if(clockout.TimeOfDay < TimeSpan.Parse("17:00:00"))
                    {
                        var attendanceds = AppDbContext.Attendance.Find(idedit);
                        attendanceds.Status = 5;
                        attendanceds.Clockout = clockout;
                        AppDbContext.SaveChanges();
                    }
                    else
                    {
                        var attendanceds = AppDbContext.Attendance.Find(idedit);
                        attendanceds.Status = 2;
                        attendanceds.Clockout = clockout;
                        AppDbContext.SaveChanges();
                    }
                    return RedirectToAction("Index", "Attendance");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Attandance/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Attandance/Edit/5
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

        // GET: Attandance/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Attandance/Delete/5
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