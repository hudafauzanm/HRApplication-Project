using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    public class Attendance
    {
        [Key]
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Clockin { get; set; }
        public DateTime Clockout { get; set; }
        public int Status { get; set; }
        public DateTime Created_at { get; set; }
    }
}
