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
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Web;

namespace RecLeagueBlog.Data.EFRepositories
{
    public class EFUserRepo : IUserRepo
    {
        RecBlogDBContext context = new RecBlogDBContext();

        public List<AppUser> GetAllUsers()
        {

            var users = context.Users.ToList();
            var roles = context.Roles.ToList();

            foreach (var u in users)
            {
                foreach (var r in u.Roles)
                {
                    if (roles.Any(ur => ur.Id == r.RoleId))
                    {
                        var roleFound = roles.First(ur=> ur.Id == r.RoleId);

                        u.RoleName = roleFound.Name;
                    }
                }

                //test.FindById(users[0].Roles.First().RoleId).Name


            }
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

        public void DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(AppUser newUser)
        {
            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}
