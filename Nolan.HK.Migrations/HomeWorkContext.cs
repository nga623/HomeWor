using System.IO;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
 
 

namespace Nolan.HK.Migrations
{
    public class HomeWorkContext1 : DbContext
    {
        public HomeWorkContext1(DbContextOptions<HomeWorkContext1> options)

            : base(options)
        {

        }
   
        public DbSet<Nolan.HK.Domain.Entities.User> User { get; set; }
        public DbSet<Nolan.HK.Domain.Entities.TimeSheetDetail> TimeSheetDetail { get; set; }
        public DbSet<Nolan.HK.Domain.Entities.TimeSheet> TimeSheet { get; set; }
        public DbSet<Nolan.HK.Domain.Entities.Project> Project { get; set; }


    }

}
