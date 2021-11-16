namespace CarDealership.UI.Migrations
{
    using CarDealership.UI.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarDealership.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarDealership.UI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //create roles
            try
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


                if (!context.Roles.Any(r => r.Name == "admin"))
                {
                    var role = new IdentityRole { Name = "admin" };
                    roleMgr.Create(role);
                }
                if (!context.Roles.Any(r => r.Name == "sales"))
                {
                    var role = new IdentityRole { Name = "sales" };
                    roleMgr.Create(role);
                }
                if (!context.Roles.Any(r => r.Name == "disabled"))
                {
                    var role = new IdentityRole { Name = "disabled" };
                    roleMgr.Create(role);
                }

                if (!context.Users.Any(u => u.UserName == "admin@test.com"))
                {
                    var user = new ApplicationUser { UserName = "admin@test.com" };
                    user.Id = "22222222 - 2222 - 2222 - 2222 - 222222222222";
                    user.FirstName = "test";
                    user.LastName = "doe";
                    user.Email = "admin@test.com";
                    

                    userMgr.Create(user, "password");
                    userMgr.AddToRole(user.Id, "admin");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
