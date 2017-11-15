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
            }
            return users;
        }
        public AppUser GetUser(string id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(AppUser updatedUser)
        {
            context.Entry(updatedUser).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            var userToDelete = context.Users.FirstOrDefault(u => u.Id == id);

            context.Users.Remove(userToDelete);
            context.SaveChanges();

        }

        public void CreateUser(AppUser newUser)
        {
            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return context.Roles.ToList();
        }
        public UserRoleViewModel ConvertUserToVM(AppUser user)
        {
            throw new NotImplementedException();
        }

        public void ConvertVMtoUserForAdd(UserRoleViewModel viewModel)
        {
            var user = viewModel.AppUser;
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void ConvertVMtoUserForEdit(UserRoleViewModel viewModel)
        {
            var user = viewModel.AppUser;
            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
