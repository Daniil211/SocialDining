using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB5_1._Database
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }
        public byte[]? Cover { get; set; }
        public User? User { get; set; }
        public Record? Record { get; set; }
        public Musician? Musician { get; set; }

    }
}
