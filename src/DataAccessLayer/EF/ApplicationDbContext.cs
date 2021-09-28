using DataAccessLayer.Adapters.ExtensionModels;
using DataAccessLayer.EF.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
            if (!string.IsNullOrEmpty(Database.GetDbConnection().ConnectionString))
            {
                ConnectionString.Value = Database.GetDbConnection().ConnectionString;
            }
        }

        public DbSet<Category> Categories { get; set; }
       
       
    }
}
