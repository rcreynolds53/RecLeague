using Microsoft.AspNet.Identity.EntityFramework;
using RecLeagueBlog.Models;
using RecLeagueBlog.Models.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Data
{
   public class RecBlogDBContext : IdentityDbContext<AppUser>
    {
        public RecBlogDBContext() : base("RecLeagueBlog")
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StaticPage> StaticPages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<RecLeagueBlog.Models.Identity.AppRole> IdentityRoles { get; set; }
    }
}
