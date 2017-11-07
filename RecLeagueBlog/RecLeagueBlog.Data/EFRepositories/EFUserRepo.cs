using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models.Identity;
using RecLeagueBlog.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace RecLeagueBlog.Data.EFRepositories
{
    public class EFUserRepo : IUserRepo
    {
        RecBlogDBContext context = new RecBlogDBContext();

        public List<AppUser> GetAllUsers()
        {
            //RoleManager<IdentityRole> test = ;

            var users = context.Users.ToList();

                //test.FindById(users[0].Roles.First().RoleId).Name

            return users;
        }

        public AppUser GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(AppUser updatedUser)
        {
            throw new NotImplementedException();
        }
    }
}
