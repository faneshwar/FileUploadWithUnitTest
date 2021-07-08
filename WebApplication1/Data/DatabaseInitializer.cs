using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Data
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            IList<Login> defaultLogins = new List<Login>
            {
                new Login() { Username = "test1", Passwrod = "Test@123" },
                new Login() { Username = "test1", Passwrod = "Test@123" }
            };
            context.Logins.AddRange(defaultLogins);
            base.Seed(context);
        }
    }
}