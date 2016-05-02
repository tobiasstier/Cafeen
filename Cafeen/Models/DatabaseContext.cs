using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace Cafeen.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<History> Histories { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Accounting> Accountings { get; set; }
    }
}