using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Data
{

    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public DbSet<Login> Logins { get; set; }
        public DbSet<User> Users { get; set; }
    }

}