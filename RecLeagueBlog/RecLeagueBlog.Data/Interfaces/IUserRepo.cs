using Microsoft.AspNet.Identity.EntityFramework;
using RecLeagueBlog.Models;
using RecLeagueBlog.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Data.Interfaces
{
    public interface IUserRepo
    {
        List<AppUser> GetAllUsers();
        AppUser GetUser(string id);
        void DeleteUser(string id);
        IEnumerable<IdentityRole> GetAllRoles();
        UserRoleViewModel ConvertUserToVM(AppUser user);
        void ConvertVMtoUserForAdd(UserRoleViewModel viewModel);
        void ConvertVMtoUserForEditAsync(UserRoleViewModel viewModel);
    }
}
