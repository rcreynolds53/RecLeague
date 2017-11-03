namespace RecLeagueBlog.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RecLeagueBlog.Models.Identity;
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


            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleMgr.RoleExists("admin"))
            {
                roleMgr.Create(new IdentityRole() { Name = "admin" });
            }
            if (userMgr.FindByName("admin@recleague.com") == null)
            {
                var newuser = new AppUser()
                {
                    FirstName = "Kipp",
                    LastName = "Graham",
                    UserName = "admin@recleague.com",
                    Email = "admin@recleague.com"

                };
                userMgr.Create(newuser, "Testing123");
            }
            var user = userMgr.FindByName("admin@recleague.com");
            if (!userMgr.IsInRole(user.Id, "admin"))
            {
                userMgr.AddToRole(user.Id, "admin");
            }


            if (!roleMgr.RoleExists("manager"))
            {
                roleMgr.Create(new IdentityRole() { Name = "manager" });
            }
            if (userMgr.FindByName("manager@recleague.com") == null)
            {
                var newuser = new AppUser()
                {
                    FirstName = "AJ",
                    LastName = "Rohde",
                    UserName = "manager@recleague.com",
                    Email = "manager@recleague.com"

                };
                userMgr.Create(newuser, "Testing456");
            }
            var userMan = userMgr.FindByName("manager@recleague.com");
            if (!userMgr.IsInRole(user.Id, "manager"))
            {
                userMgr.AddToRole(user.Id, "manager");
            }
        }
    }
}
