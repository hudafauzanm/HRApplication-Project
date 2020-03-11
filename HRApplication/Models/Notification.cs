using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    public class Notification
    {
        public Guid id { get; set; }
        public string sender_id { get; set; }
        public int role_id { get; set; }
        public DateTime read_at { get; set; }
        public DateTime created_at { get; set; }
    }
}
