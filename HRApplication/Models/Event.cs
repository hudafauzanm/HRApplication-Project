using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public DateTime TimeEvent { get; set; }
        public DateTime Created_At { get; set; } 
    }
}
