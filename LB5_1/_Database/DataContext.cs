using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB5_1._Database
{
    public class DataContext : DbContext
    {
        public DataContext() : base("StudioRecordings") { }
        public DbSet<User> Users { get; set; }
        public DbSet<DiningRoom> Studios { get; set; }
        public DbSet<Item> Records { get; set; }
        public DbSet<СomboSet> Projects { get; set; }
        public DbSet<Admin> Musicians { get; set; }
    }
}
