namespace RecLeagueBlog.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RecLeagueBlog.Data.RecBlogDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RecLeagueBlog.Data.RecBlogDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Load the user and role managers with our custom models
            var userMgr = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // have we loaded roles already?
            if (roleMgr.RoleExists("admin"))
                return;

            // create the admin role
            roleMgr.Create(new IdentityRole() { Name = "admin" });


            // create the default user
            var user = new IdentityUser()
            {
                UserName = "admin@recleague.com",
                Email = "admin@recleague.com"
            };



            // create the user with the manager class
            userMgr.Create(user, "Testing123");

            // add the user to the admin role
            userMgr.AddToRole(user.Id, "admin");


            if (roleMgr.RoleExists("manager"))
                return;

            roleMgr.Create(new IdentityRole() { Name = "manager" });

            var lesserUser = new IdentityUser()
            {
                UserName = "manager@recleague.com",
                Email = "manager@recleague.com",
            };

            userMgr.Create(lesserUser, "Testing456");
            userMgr.AddToRole(lesserUser.Id, "manager");
        }
    }
}
