﻿using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecLeagueBlog.Models;

namespace RecLeagueBlog.Data.MockRepositories
{
    public class MockUserRepository : IUserRepo
    {
        private static List<AppUser> _users;
        private static List<AppRole> _roles;
        static MockUserRepository()
        {
            _roles = new List<AppRole>()
            {
                new AppRole {Id = "1", Name = "admin"},
                new AppRole {Id = "2", Name = "manager"}
            };
            _users = new List<AppUser>()
            {
                new AppUser {FirstName = "Drew", LastName ="K", Email = "drew@recleague.com", Id = "1"},
                new AppUser {FirstName = "Mark", LastName ="J", Email = "mark@recleague.com", Id = "2"},
                new AppUser {FirstName = "Alex", LastName ="C", Email = "alex@recleague.com", Id = "3"},
                new AppUser {FirstName = "Rob", LastName ="R", Email = "rob@recleague.com", Id = "5"},

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

        //public void UpdateUser(AppUser updatedUser)
        //{
        //    _users.RemoveAll(u => u.Id == updatedUser.Id);
        //    _users.Add(updatedUser);
        //}

        public void DeleteUser(string id)
        {
            _users.RemoveAll(u => u.Id == id);
        }

        //public void CreateUser(AppUser newUser)
        //{
        //    _users.Add(newUser);
        //}

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roles;
        }

        public UserRoleViewModel ConvertUserToVM(AppUser user)
        {
            UserRoleViewModel viewModel = new UserRoleViewModel();
            viewModel.AppUser = user;
            return viewModel;
        }
        public void ConvertVMtoUserForAdd(UserRoleViewModel viewModel)
        { 
            var user = viewModel.AppUser;
            _users.Add(user);

        }
        public void ConvertVMtoUserForEditAsync(UserRoleViewModel viewModel)
        {
            var user = viewModel.AppUser;
            _users.RemoveAll(u => u.Id == viewModel.AppUser.Id);
            _users.Add(user);
        }
    }
}
