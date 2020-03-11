using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using HRApplication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HRApplication.Controllers
{
    public class BroadcastController : Controller
    {
        public IConfiguration Configuration;
        public AppDbContext AppDbContext { get; set; }

        public BroadcastController(IConfiguration configuration, AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Configuration = configuration;
        }
        // GET: Broadcast
        public ActionResult Index()
        {
            var notif = (from e in AppDbContext.LeaveRequest where e.Read_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e).Count();
            ViewBag.Notif = notif;
            return View("Broadcast");
        }

        // GET: Broadcast/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Broadcast/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Broadcast/Create
        [HttpPost]
        public ActionResult Create(string Message)
        {
            try
            {
                List<string> emails = new List<string>();
                var email = from e in AppDbContext.Employee select e;
                foreach (var e in email)
                {
                    emails.Add(e.Email);                    
                }
                Thread task = new Thread(() => MailService(emails, Message));
                task.Start();
                return RedirectToAction("Index","Broadcast");
            }
            catch
            {
                return View();
            }
        }

        // GET: Broadcast/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Broadcast/Edit/5
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

        // GET: Broadcast/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Broadcast/Delete/5
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

        public void MailService(List<string> email,string messages)
        {
            foreach(var x in email)
            {
                MailAddress to = new MailAddress(x);
                MailAddress from = new MailAddress("adminHRBERLIAN-HUCA@HUCA.com");
                MailMessage message = new MailMessage(from, to);
                message.Subject = "BERLIAN-HUCA-ADMIN";
                message.Body = $@"<p>Hey {x},<br>
                            <h3>INFORMASI PEGAWAI</h3><br>
                            <p>Message : {messages}<br>
                            <p>Thankyou<br>
                            <p>-- ADMIN<br>";
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.mailtrap.io", 587)
                {
                    Credentials = new NetworkCredential("00d7115f88c37d", "2d3d930201d6ed"),
                    EnableSsl = true
                };
                client.Send(message);
            }
        }
    }
}