using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB5_1._Database
{
    public class DiningRoom
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Time { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Item>? Items { get; set; }
        public List<Admin>? Admins { get; set; }
    }
}
