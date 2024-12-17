using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sales_web_mvc.Models;

namespace sales_web_mvc.Data
{
    public class sales_web_mvcContext : DbContext
    {
        public sales_web_mvcContext (DbContextOptions<sales_web_mvcContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<SalesRecord> SalesRecords { get; set; }
    }
}
