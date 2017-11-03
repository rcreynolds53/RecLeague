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
        void UpdateUser(AppUser updatedUser);
    }
}
