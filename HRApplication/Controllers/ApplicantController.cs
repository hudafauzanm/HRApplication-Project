using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRApplication.Data;
using HRApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HRApplication.Controllers
{
    public class ApplicantController : Controller
    {
        public IConfiguration Configuration;
        public AppDbContext AppDbContext { get; set; }

        public ApplicantController(IConfiguration configuration, AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Configuration = configuration;
        }
        [Authorize]
        // GET: Applicant
        public ActionResult Index()
        {
            var unprocessed = from e in AppDbContext.Applicant where e.Status.Contains("unprocessed") && e.Move_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e;
            var process = from e in AppDbContext.Applicant where e.Status.Contains("psychotest") && e.Move_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e;
            var interview = from e in AppDbContext.Applicant where e.Status.Contains("interview") && e.Move_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e;
            var notif = (from e in AppDbContext.LeaveRequest where e.Read_at == DateTime.Parse("0001-01-01 00:00:00.0000000") select e).Count();
            ViewBag.Notif = notif;
            ViewBag.Un = unprocessed;
            ViewBag.Pro = process;
            ViewBag.Inter = interview;
            return View("Applicant");
        }

        // GET: Applicant/Details/5
        public ActionResult Add()
        {
            return View("AddApplicant");
        }

        // GET: Applicant/Create
        public ActionResult Create()
        {
            return View();
        }
        public FileResult ExportAll()
        {
            var export_appl = from ex in AppDbContext.Applicant select ex;
            string[] colomnama = new string[15] { "Applicant ID", "FullName", "Email", "Position", "Division", "Status", "Phone", "Birthplace", "Gender", "Emergency Contact", "Photo", "Birthdate", "Move_at", "Created", "Address" };
            string csv = string.Empty;

            foreach (string columnName in colomnama)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var appl in export_appl)
            {
                //Add the Data rows.
                csv += Convert.ToString(appl.Id).Replace(",", ";") + ',';
                csv += appl.Fullname.Replace(",", ";") + ',';
                csv += appl.Email.Replace(",", ";") + ',';
                csv += appl.Position.Replace(",", ";") + ',';
                csv += appl.Division.Replace(",", ";") + ',';
                csv += appl.Status.Replace(",", ";") + ',';
                csv += appl.Phone_number.Replace(",", ";") + ',';
                csv += appl.Birthplace.Replace(",", ";") + ',';
                csv += appl.Gender;
                csv += ",";
                if(appl._emergencyContact == null)
                {
                    csv += "Null";
                    csv += ",";
                }
                else
                {
                    Console.WriteLine("Kok masuk sini");
                    csv += appl._emergencyContact.Replace(",", ";") + ',';
                }
                if (appl.Photo == null)
                {
                    Console.WriteLine("masuk sini");
                    csv += "Null";
                    csv += ",";

                }
                else
                {
                    csv += appl.Photo.Replace(",", ";") + ',';
                }
                if (appl.CV == null)
                {
                    csv += "Null";
                    csv += ",";
                }
                else
                {
                    csv += appl.CV.Replace(",", ";") + ',';
                }
                csv += appl.Birthdate;
                csv += ",";
                csv += appl.Move_at;
                csv += ",";
                csv += appl.Created_at;
                csv += ",";
                csv += Convert.ToString(appl.Address).Replace("\n", " ");
                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "ApplicantAll.csv");
        }
        public FileResult Export(string status,string search)
        {
            Console.WriteLine(status);
            var export_appl = from ex in AppDbContext.Applicant where ex.Status.Contains(status) || ex.Fullname.Contains(search) select ex;
            string[] colomnama = new string[15] { "Applicant ID", "FullName", "Email", "Position", "Division", "Status", "Phone", "Birthplace", "Gender", "Emergency Contact", "Photo", "Birthdate", "Move_at", "Created", "Address" };
            string csv = string.Empty;

            foreach (string columnName in colomnama)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var appl in export_appl)
            {
                csv += Convert.ToString(appl.Id).Replace(",", ";") + ',';
                csv += appl.Fullname.Replace(",", ";") + ',';
                csv += appl.Email.Replace(",", ";") + ',';
                csv += appl.Position.Replace(",", ";") + ',';
                csv += appl.Division.Replace(",", ";") + ',';
                csv += appl.Status.Replace(",", ";") + ',';
                csv += appl.Phone_number.Replace(",", ";") + ',';
                csv += appl.Birthplace.Replace(",", ";") + ',';
                csv += appl.Gender;
                csv += ",";
                if (appl._emergencyContact == null)
                {
                    csv += "Null";
                    csv += ",";
                }
                else
                {
                    csv += appl._emergencyContact.Replace(",", ";") + ',';
                }
                if (appl.Photo == null)
                {
                    csv += "Null";
                    csv += ",";

                }
                else
                {
                    csv += appl.Photo.Replace(",", ";") + ',';
                }
                if (appl.CV == null)
                {
                    csv += "Null";
                    csv += ",";

                }
                else
                {
                    csv += appl.CV.Replace(",", ";") + ',';
                }
                csv += appl.Birthdate;
                csv += ",";
                csv += appl.Move_at;
                csv += ",";
                csv += appl.Created_at;
                csv += ",";
                csv += Convert.ToString(appl.Address).Replace("\n", " ");
                csv += "\r\n";
            }

            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", $"Applicant_{status}{search}.csv");
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

                            Applicant applicant = new Applicant()
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

                            AppDbContext.Applicant.Add(applicant);
                        }
                        AppDbContext.SaveChanges();
                    }
                    return RedirectToAction("Index", "Applicant");
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
            csv += "Unprocessed/Psychotest/Interview" + ",";
            csv += "08123123123" + ',';
            csv += "Klaten/Jogja" + ',';
            csv += "1/2" + ',';
            csv += "MM/DD/YYYY" + ',';
            csv += "ALAMAT";            

            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "ApplicantTemp.csv");
        }
        // POST: Applicant/Create
        [HttpPost]
        public ActionResult Create(string Fullname, string Email, string Phone, int Gender, DateTime Birthdate, string Birthplace, string Address, string Position, string Division, string emername, string emerphone, [FromForm(Name = "Photo")]IFormFile Photo, [FromForm(Name = "CV")]IFormFile CV)
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
                if(!System.IO.Directory.Exists("wwwroot" + "/File/"))
                {
                    System.IO.Directory.CreateDirectory("wwwroot" + "/File/");
                }
                string storePath = "wwwroot/Image/";
                string filePath = "wwwroot/File/";
                if (Photo != null && CV != null)
                {
                    Console.WriteLine("Masuk sini juga");
                    var path = Path.Combine(storePath, Photo.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Photo.CopyTo(stream);
                    }
                    var paths = Path.Combine(filePath, CV.FileName);
                    using (var stream = new FileStream(paths,FileMode.Create))
                    {
                        CV.CopyTo(stream);
                    }
                    var pathfix = path.Substring(8);
                    var cvpath = paths.Substring(8);
                    Applicant applicant = new Applicant()
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
                        Status = "unprocessed",
                        Emergencycontact = emergency,
                        Photo = pathfix,
                        CV = cvpath,
                        Created_at = DateTime.Now,
                    };
                    AppDbContext.Applicant.Add(applicant);
                }
                AppDbContext.SaveChanges();
                return RedirectToAction("Index", "Applicant");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        // GET: Applicant/Edit/5
        public ActionResult Edit(string id)
        {
            var emp = from e in AppDbContext.Applicant where e.Id == Guid.Parse(id) select e;
            var empdata = (from x in AppDbContext.Applicant where x.Id == Guid.Parse(id) select x.Birthdate).Distinct();
            string bd = "";
            foreach (var date in empdata)
            {
                bd = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            var dated = bd.Substring(0, 10).Replace("/", "-");
            ViewBag.Date = dated;
            ViewBag.Emp = emp;
            return View("EditApplicant");
        }

        // POST: Applicant/Edit/5
        [HttpPost]
        public ActionResult Edit(string Id,string Fullname, string Email, string Phone, int Gender, DateTime Birthdate, string Birthplace, string Address, string Position, string Division,string Status, string emername, string emerphone, [FromForm(Name = "Photo")]IFormFile Photo, [FromForm(Name = "CV")]IFormFile CV)
        {
            try
            {
                List<string> emergency = new List<string>();
                emergency.Add(emername + "," + emerphone);
                if (!System.IO.Directory.Exists("wwwroot" + "/Image/"))
                {
                    System.IO.Directory.CreateDirectory("wwwroot" + "/Image/");
                }
                if (!System.IO.Directory.Exists("wwwroot" + "/File/"))
                {
                    System.IO.Directory.CreateDirectory("wwwroot" + "/File/");
                }
                string storePath = "wwwroot/Image/";
                string filePath = "wwwroot/File/";
                if (Photo != null && CV != null)
                {
                    Console.WriteLine("Masuk sini juga");
                    var path = Path.Combine(storePath, Photo.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Photo.CopyTo(stream);
                    }
                    var paths = Path.Combine(filePath, CV.FileName);
                    using (var stream = new FileStream(paths, FileMode.Create))
                    {
                        CV.CopyTo(stream);
                    }
                    var pathfix = path.Substring(8);
                    var cvpath = paths.Substring(8);
                    var idedit = Guid.Parse(Id);
                    var edit = AppDbContext.Applicant.Find(idedit);
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
                    edit.CV = cvpath;
                }
                Console.WriteLine("MASUK SINI BERARTI");
                var idedits = Guid.Parse(Id);
                var edits = AppDbContext.Applicant.Find(idedits);
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
                return RedirectToAction("Index", "Applicant");
            }
            catch
            {
                return View();
            }
        }

        // GET: Applicant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Applicant/Delete/5
        [HttpPost]
        public ActionResult Delete(string deleteid)
        {
            try
            {
                var idedits = Guid.Parse(deleteid);
                var edits = AppDbContext.Applicant.Find(idedits);
                edits.Move_at = DateTime.Now;
                AppDbContext.SaveChanges();
                return RedirectToAction("Index", "Applicant");
            }
            catch
            {
                return View();
            }
        }
    }
}