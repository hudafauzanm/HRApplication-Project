using HRApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<Employe> Employee { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<User> User { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
