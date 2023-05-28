using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB5_1._Database
{
    public class Studio
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Time { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Record>? Records { get; set; }
        public List<Musician>? Musicians { get; set; }
    }
}
