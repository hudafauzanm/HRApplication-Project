using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.Data;
using HRApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HRApplication.Controllers
{
    public class EventController : Controller
    {
        public IConfiguration Configuration;
        public AppDbContext AppDbContext { get; set; }

        public EventController(IConfiguration configuration, AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Configuration = configuration;
        }
        [Authorize]
        // GET: Event
        public ActionResult Index()
        {
            var events = from x in AppDbContext.Event select x;
            ViewBag.Event = events;
            var get = HttpContext.Session.GetString("Name");
            ViewBag.Name = get;
            return View("Event");
        }

        // GET: Event/Details/5
        public ActionResult Event()
        {
            return View("AddEvent");
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(string Eventname,DateTime Eventdate)
        {
            try
            {
                Event eventual = new Event
                {
                    EventName = Eventname,
                    TimeEvent = Eventdate,
                    Created_At = DateTime.Now
                };
                AppDbContext.Event.Add(eventual);
                AppDbContext.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Edit/5
        public ActionResult Edit(string Id)
        {
            var events = from x in AppDbContext.Event where x.Id == Guid.Parse(Id) select x;
            string bd = "";
            foreach (var date in events)
            {
                bd = date.TimeEvent.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            var dated = bd.Substring(0, 10).Replace("/", "-");
            ViewBag.Date = dated;
            ViewBag.Event = events;
            return View("EditEvent");
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(string Id,string Eventname,DateTime Eventdate)
        {
            try
            {
                var idedits = Guid.Parse(Id);
                var events = AppDbContext.Event.Find(idedits);
                events.EventName = Eventname;
                events.TimeEvent = Eventdate;
                AppDbContext.SaveChanges();

                return RedirectToAction("Index","Event");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(string deleteid)
        {
            try
            {
                Console.WriteLine(deleteid);
                var idedits = Guid.Parse(deleteid);
                var edits = AppDbContext.Event.Find(idedits);
                AppDbContext.Remove(edits);
                AppDbContext.SaveChanges();
                return RedirectToAction("Index", "Event");
            }
            catch
            {
                return View();
            }
        }
    }
}