using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Attack_And_Defend.Models;

namespace Attack_And_Defend.Data
{
    public class UserApplicationContext : IUserContext
    {
        EFContext context;

        public UserApplicationContext(EFContext context)
        {
            this.context = context;
        }

        public bool CheckUserExists(string username)
        {
            return context.Users.Any(u => u.UserName == username);
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return context.ApplicationUsers.ToList();
        }

        public void Finish()
        {
            context.SaveChanges();
        }

    }
}
