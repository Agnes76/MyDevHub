using Microsoft.EntityFrameworkCore;
using MyDevHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcTaxApp.Services
{
    public class MyHubContext : DbContext
    {
        public MyHubContext(DbContextOptions<MyHubContext> dbContext): base(dbContext)
        {
            
        }

        public DbSet<ImageFile> ImageFiles { get; set; }
    }
}
