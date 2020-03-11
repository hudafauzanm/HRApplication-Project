using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    public class LeaveRequest
    {
        [Key]
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public DateTime LeaveTime  { get; set; }
        public DateTime Read_at { get; set; }
        public DateTime Created_at { get; set; }
    }
}
