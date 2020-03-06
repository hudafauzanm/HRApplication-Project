using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    public class Employe
    {
        [Key]
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        [Column(TypeName = "text")]
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string Birthplace { get; set; }
        public string Position { get; set; }
        public string Division { get; set; }
        public string Phone_number { get; set; }
        public string Photo { get; set; }
        public string _emergencyContact { get; set; }
        public string Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Resign_at { get; set; }

        [NotMapped]
        public List<string> Emergencycontact
        {
            get { return _emergencyContact == null ? null : JsonConvert.DeserializeObject<List<string>>(_emergencyContact); }
            set
            {
                _emergencyContact = JsonConvert.SerializeObject(value);
            }
        }
    }
}
