using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRApplication.Data;
using HRApplication.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        // GET: Employee
        public ActionResult Index()
        {
            var contract = from e in AppDbContext.Employee where e.Status.Contains("Contract") && e.Resign_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e;
            var permanent = from e in AppDbContext.Employee where e.Status.Contains("Permanent") && e.Resign_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e;
            var probation = from e in AppDbContext.Employee where e.Status.Contains("Probation") && e.Resign_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e;
            var notif = (from e in AppDbContext.LeaveRequest where e.Read_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e).Count();
            ViewBag.Notif = notif;
            ViewBag.Contract = contract;
            ViewBag.Permanent = permanent;
            ViewBag.Probation = probation;
            var get = HttpContext.Session.GetString("Name");
            ViewBag.Name = get;
            return View("Employee");
        }
        public FileResult ExportAll()
        {
            var export_emp = from ex in AppDbContext.Employee select ex;
            string[] colomnama = new string[15] { "Employee ID", "FullName", "Email", "Position", "Division", "Status", "Phone", "Birthplace", "Gender", "Emergency Contact", "Photo", "Birthdate", "Resign_at", "Created", "Address" };
            string csv = string.Empty;

            foreach (string columnName in colomnama)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var emp in export_emp)
            {
                //Add the Data rows.
                csv += Convert.ToString(emp.Id).Replace(",", ";") + ',';
                csv += emp.Fullname.Replace(",", ";") + ',';
                csv += emp.Email.Replace(",", ";") + ',';
                csv += emp.Position.Replace(",", ";") + ',';
                csv += emp.Division.Replace(",", ";") + ',';
                csv += emp.Status.Replace(",", ";") + ',';
                csv += emp.Phone_number.Replace(",", ";") + ',';
                csv += emp.Birthplace.Replace(",", ";") + ',';
                csv += emp.Gender;
                csv += ",";
                if (emp._emergencyContact == null)
                {
                    csv += "Null";
                    csv += ",";
                }
                else
                {
                    csv += emp._emergencyContact.Replace(",", ";") + ',';
                }
                if (emp.Photo == null)
                {
                    csv += "Null";
                    csv += ",";

                }
                else
                {
                    csv += emp.Photo.Replace(",", ";") + ',';
                }
                csv += emp.Birthdate;
                csv += ",";
                csv += emp.Resign_at;
                csv += ",";
                csv += emp.Created_at;
                csv += ",";
                csv += Convert.ToString(emp.Address);
                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "EmployeeAll.csv");
        }
        public FileResult Export(string status, string search)
        {
            var export_emp = from ex in AppDbContext.Employee where ex.Status.Contains(status) || ex.Fullname.Contains(search) select ex;
            string[] colomnama = new string[15] { "Employee ID", "FullName", "Email", "Position", "Division", "Status", "Phone", "Birthplace", "Gender", "Emergency Contact", "Photo", "Birthdate", "Resign_at", "Created", "Address" };
            string csv = string.Empty;

            foreach (string columnName in colomnama)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var emp in export_emp)
            {
                //Add the Data rows.
                csv += Convert.ToString(emp.Id).Replace(",", ";") + ',';
                csv += emp.Fullname.Replace(",", ";") + ',';
                csv += emp.Email.Replace(",", ";") + ',';
                csv += emp.Position.Replace(",", ";") + ',';
                csv += emp.Division.Replace(",", ";") + ',';
                csv += emp.Status.Replace(",", ";") + ',';
                csv += emp.Phone_number.Replace(",", ";") + ',';
                csv += emp.Birthplace.Replace(",", ";") + ',';
                csv += emp.Gender;
                csv += ",";
                if (emp._emergencyContact == null)
                {
                    csv += "Null";
                    csv += ",";
                }
                else
                {
                    csv += emp._emergencyContact.Replace(",", ";") + ',';
                }
                if (emp.Photo == null)
                {
                    csv += "Null";
                    csv += ",";

                }
                else
                {
                    csv += emp.Photo.Replace(",", ";") + ',';
                }
                csv += emp.Birthdate;
                csv += ",";
                csv += emp.Resign_at;
                csv += ",";
                csv += emp.Created_at;
                csv += ",";
                csv += Convert.ToString(emp.Address);
                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", $"Employee{status}{search}.csv");
        }
        public FileResult ImportTemp()
        {
            var export_emp = from ex in AppDbContext.Employee select ex;
            string[] colomnama = new string[10] { "FullName", "Email", "Position", "Division", "Status", "Phone", "Birthplace", "Gender", "Birthdate", "Address" };
            string csv = string.Empty;

            foreach (string columnName in colomnama)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";
            csv += "Nama" + ',';
            csv += "Email@Email" + ',';
            csv += "System Developer" + ',';
            csv += "ISA" + ',';
            csv += "Permanent/Probation/Contract" + ",";
            csv += "08123123123" + ',';
            csv += "Klaten/Jogja" + ',';
            csv += "1/2" + ',';
            csv += "MM/DD/YYYY" + ',';
            csv += "ALAMAT";

            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "EmployeeTemp.csv");
        }
        public IActionResult Import([FromForm(Name = "Upload")] IFormFile Upload)
        {
            string filePath = string.Empty;
            Console.WriteLine(Upload.FileName);
            if (Upload != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(Upload.FileName);

                    //Validate uploaded file and return error.
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select the csv file with .csv extension";
                        Console.WriteLine("Bukan");
                        return RedirectToAction("Index", "Admin");
                    }
                    using (var sreader = new StreamReader(Upload.OpenReadStream()))
                    {
                        string[] headers = sreader.ReadLine().Split(',');

                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');

                            Employe employee = new Employe()
                            {
                                Fullname = rows[0].ToString(),
                                Email = rows[1].ToString(),
                                Position = rows[2].ToString(),
                                Division = rows[3].ToString(),
                                Status = rows[4].ToString(),
                                Phone_number = rows[5].ToString(),
                                Birthplace = rows[6].ToString(),
                                Gender = int.Parse(rows[7]),
                                Birthdate = DateTime.Parse(rows[8]),
                                Address = rows[9],
                                Created_at = DateTime.Now
                            };

                            AppDbContext.Employee.Add(employee);
                        }
                        AppDbContext.SaveChanges();
                    }
                    return RedirectToAction("Index", "Employee");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                Console.WriteLine("GAGAL MASUK 1");
                ViewBag.Message = "Please select the file first to upload.";
            }
            Console.WriteLine("GAGAL MASUK 2");
            return RedirectToAction("Index", "Employee");
        }
        // GET: Employee
        [Authorize]
        public ActionResult Add(string id)
        {
            if (String.IsNullOrEmpty(id) == true)
            {
                ViewBag.App = "";
            }
            else
            {
                var app = from a in AppDbContext.Applicant where a.Id == Guid.Parse(id) select a;
                var empdata = (from x in AppDbContext.Applicant where x.Id == Guid.Parse(id) select x.Birthdate).Distinct();
                var idguid = Guid.Parse(id);
                var update = AppDbContext.Applicant.Find(idguid);
                update.Move_at = DateTime.Now;
                update.Status = "finish";
                string bd = "";
                foreach (var date in empdata)
                {
                    bd = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                var dated = bd.Substring(0, 10).Replace("/", "-");
                ViewBag.Date = dated;
                ViewBag.Apps = app;
                AppDbContext.SaveChanges();
            }
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
        public ActionResult Create(string Fullname, string Email, string Phone, string Status, int Gender, DateTime Birthdate, string Birthplace, string Address, string Position, string Division, string emername, string emerphone, [FromForm(Name = "Photo")]IFormFile Photo)
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
                return RedirectToAction("Index", "Employee");
            }
            catch
            {
                return View("Employee");
            }
        }
        // POST: Employee/Create
        [HttpPost]
        public ActionResult Add(string Fullname, string Email, string Phone, string Status, int Gender, DateTime Birthdate, string Birthplace, string Address, string Position, string Division, string emername, string emerphone, [FromForm(Name = "Photo")]IFormFile Photo)
        {
            try
            {
               
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

        [Authorize]
        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            var emp = from e in AppDbContext.Employee where e.Id == Guid.Parse(id) select e;
            var empdata = (from x in AppDbContext.Employee where x.Id == Guid.Parse(id) select x.Birthdate).Distinct();
            string bd = "";
            foreach (var date in empdata)
            {
                bd = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            var dated = bd.Substring(0, 10).Replace("/", "-");
            ViewBag.Date = dated;
            ViewBag.Emp = emp;
            return View("EditEmployee");
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(string Id, string Fullname, string Email, string Phone, string Status, int Gender, DateTime Birthdate, string Birthplace, string Address, string Position, string Division, string emername, string emerphone, [FromForm(Name = "Photo")]IFormFile Photo)
        {
            try
            {      
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
                    var path = Path.Combine(storePath, Photo.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Photo.CopyTo(stream);
                    }
                    var pathfix = path.Substring(8);
                    var idedit = Guid.Parse(Id);
                    var edit = AppDbContext.Employee.Find(idedit);
                    edit.Fullname = Fullname;
                    edit.Email = Email;
                    edit.Phone_number = Phone;
                    edit.Address = Address;
                    edit.Birthdate = Birthdate;
                    edit.Birthplace = Birthplace;
                    edit.Gender = Gender;
                    edit.Position = Position;
                    edit.Division = Division;
                    edit.Emergencycontact = emergency;
                    edit.Status = Status;
                    edit.Photo = pathfix;
                }
                var idedits = Guid.Parse(Id);
                var edits = AppDbContext.Employee.Find(idedits);
                edits.Fullname = Fullname;
                edits.Email = Email;
                edits.Phone_number = Phone;
                edits.Address = Address;
                edits.Birthdate = Birthdate;
                edits.Birthplace = Birthplace;
                edits.Gender = Gender;
                edits.Position = Position;
                edits.Division = Division;
                edits.Emergencycontact = emergency;
                edits.Status = Status;
                AppDbContext.SaveChanges();
                return RedirectToAction("Index", "Employee");
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
        public ActionResult Delete(string deleteid)
        {
            try
            {
                Console.WriteLine(deleteid);
                var idedits = Guid.Parse(deleteid);
                var edits = AppDbContext.Employee.Find(idedits);
                edits.Resign_at = DateTime.Now;
                AppDbContext.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult SendEmail(string id)
        {            
            List<string> emails = new List<string>();
            var email = from e in AppDbContext.Employee where e.Id == Guid.Parse(id) select e;
            foreach (var e in email)
            {
                emails.Add(e.Email);
            }
            Thread task = new Thread(() => MailService(emails));
            task.Start();
            return RedirectToAction("Index", "Employee");
        }

        public void MailService(List<string> email)
        {
            foreach (var x in email)
            {
                MailAddress to = new MailAddress(x);
                MailAddress from = new MailAddress("adminHRBERLIAN-HUCA@HUCA.com");
                MailMessage message = new MailMessage(from, to);
                message.Subject = "BERLIAN-HUCA-ADMIN";
                message.Body = $@"<p>Hey {x},<br>
                            <h3>INFORMASI PEGAWAI</h3><br>
                            <p>Message : ANDA MENDAPATKAN SURAT PERINGATAN DIKARENAKAN ANDA MELAKUKAN SESUATU HAL<br>
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