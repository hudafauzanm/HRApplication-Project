using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HRApplication.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration;
        public AppDbContext AppDbContext { get; set; }

        public HomeController(IConfiguration configuration, AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Configuration = configuration;
        }
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            var employee = (from x in AppDbContext.Employee select x).Count();
            var emppria = (from x in AppDbContext.Employee where x.Gender == 1 select x).Count();
            var empwanita = (from x in AppDbContext.Employee where x.Gender == 2 select x).Count();
            var notif = (from e in AppDbContext.LeaveRequest where e.Read_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e).Count();
            var outs = (from e in AppDbContext.LeaveRequest where e.LeaveTime.Date == DateTime.Today.Date && e.Status == "approve" select e).Count();
            var attendance = (from e in AppDbContext.Attendance where e.Clockin.Date == DateTime.Today.Date select e).Count();
            var applicant = from x in AppDbContext.Applicant where x.Status.Contains("unprocessed") && x.Move_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select x;
            var eventual = from x in AppDbContext.Event where x.TimeEvent.Date >= DateTime.Today.Date && x.TimeEvent.Year == DateTime.Today.Year orderby x.TimeEvent ascending select x;
            var get = HttpContext.Session.GetString("Name");
            ViewBag.Name = get;
            ViewBag.Event = eventual;
            ViewBag.Appl = applicant;
            ViewBag.Out = outs;
            ViewBag.Att = attendance;
            ViewBag.Pria = emppria;
            ViewBag.Wanita = empwanita;
            ViewBag.Emp = employee;
            ViewBag.Notif = notif;
            return View("Index");
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
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