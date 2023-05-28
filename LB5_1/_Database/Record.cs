using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB5_1._Database
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
        public Studio? Studio { get; set; }
        public List<Project>? Projects { get; set; }
    }
}
