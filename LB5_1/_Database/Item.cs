using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB5_1._Database
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public DiningRoom? DiningRoom { get; set; }
        public СomboSet? СomboSet { get; set; }
    }
}