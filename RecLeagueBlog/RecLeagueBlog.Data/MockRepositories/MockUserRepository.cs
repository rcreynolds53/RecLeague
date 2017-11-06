using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models.Identity;

namespace RecLeagueBlog.Data.MockRepositories
{
    public class MockUserRepository : IUserRepo
    {
        private List<AppUser> _users;
        private List<AppRole> _roles;
        public MockUserRepository()
        {
            _roles = new List<AppRole>()
            {
                new AppRole {Id = "1", Name = "admin"},
                new AppRole {Id = "2", Name = "manager"}
            };
            _users = new List<AppUser>()
            {
                new AppUser {FirstName = "Drew", LastName ="K", Email = "drew@recleague.com"},
                new AppUser {FirstName = "Mark", LastName ="J", Email = "mark@recleague.com"},
                new AppUser {FirstName = "Alex", LastName ="C", Email = "alex@recleague.com"},
                new AppUser {FirstName = "Rob", LastName ="R", Email = "rob@recleague.com"},

            };
        }
        public List<AppUser> GetAllUsers()
        {
            return _users;
        }

        public AppUser GetUser(string id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(AppUser updatedUser)
        {
            _users.RemoveAll(u => u.Id == updatedUser.Id);
            _users.Add(updatedUser);
        }

        public void DeleteUser(string id)
        {
            _users.RemoveAll(u => u.Id == id);
        }
    }
}
