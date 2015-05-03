using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Sample.Core.Db.Context;
using Sample.Core.Db.DbModels;
using Sample.Core.Infrastructure.Security;

namespace Sample.Core.Db.Migrations
{
    public class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Db\Migrations";
        }

        protected override void Seed(AppDbContext context)
        {
            // for demonstration purpose only
            context.Set<User>().AddOrUpdate(x => x.Email, new User
            {
                Created = DateTime.Now,
                Email = "admin@sample.cz",
                FirstName = "Jméno",
                LastName = "Pøijemní",
                Password = Crypto.ComputeHash("admin")
            }); 
            context.SaveChanges();
        }
    }
}
