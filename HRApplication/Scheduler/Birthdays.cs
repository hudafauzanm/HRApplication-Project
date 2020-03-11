using HRApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace HRApplication.Scheduler
{
    public class Birthdays: IHostedService
    {
        public IConfiguration Configuration;
        private IServiceScopeFactory Services { get; set; }

        public Birthdays(IConfiguration configuration, IServiceScopeFactory services)
        {
            Services = services;            
            Configuration = configuration;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            
            Timer timer = new Timer(BirthdayGreetings, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            Timer timers = new Timer(Eventual, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stop background process");

            return Task.CompletedTask;
        }
        public List<string> Eventuals()
        {
            using (var scope = Services.CreateScope())
            {
                var myScopedService = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                List<string> email = new List<string>();
                var emails = from x in myScopedService.Employee select x.Email;
                foreach (var e in emails)
                {
                    email.Add(e);
                }
                return email;
            }
        }
        public void BirthdayGreetings(object state)
        {
            if(DateTime.Now.ToString("HH:mm").Substring(0,2) == "07")
            {
                using (var scope = Services.CreateScope())
                {
                    var myScopedService = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    List<string> email = new List<string>();
                    var birthdayboy = from x in myScopedService.Employee where x.Birthdate.Date == DateTime.Today.Date select x;
                    foreach (var x in birthdayboy)
                    {
                        MailAddress to = new MailAddress(x.Email);
                        MailAddress from = new MailAddress("adminHRBERLIAN-HUCA@HUCA.com");
                        MailMessage message = new MailMessage(from, to);
                        message.Subject = "BERLIAN-HUCA-ADMIN";
                        message.Body = $@"<p>Hey {x.Email},<br>
                        <h3>INFORMASI PEGAWAI</h3><br>
                        <p>Message : Selamat Ulang Tahun {x.Fullname}<br>
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
        public void Eventual(object state)
        {
            if (DateTime.Now.ToString("HH:mm").Substring(0, 2) == "07")
            {
                using (var scope = Services.CreateScope())
                {
                    var myScopedService = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var events = from x in myScopedService.Event select x;
                    foreach (var e in events)
                    {
                        if(DateTime.Today.AddDays(3) == e.TimeEvent)
                        {
                            var eve = Eventuals();
                            foreach(var ev in eve)
                            {
                                MailAddress to = new MailAddress(ev);
                                MailAddress from = new MailAddress("adminHRBERLIAN-HUCA@HUCA.com");
                                MailMessage message = new MailMessage(from, to);
                                message.Subject = "BERLIAN-HUCA-ADMIN";
                                message.Body = $@"<p>Hey {ev},<br>
                                <h3>INFORMASI PEGAWAI</h3><br>
                                <p>Message : Selamat Hari {e.EventName}<br>
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
            }               
            
        }
    }
}
