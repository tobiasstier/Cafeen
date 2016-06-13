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
        public DbSet<Accounting> Accountings { get; set; }
    }
}