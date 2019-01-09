using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Attack_And_Defend.Models;
using Attack_And_Defend.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Attack_And_Defend.Data
{
    public class UserRepository
    {
        IUserContext context;

        public UserRepository(IUserContext context)
        {
            this.context = context;
        }

        public bool CheckUserExists(string username)
        {
            return context.CheckUserExists(username);
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return context.GetAllUsers();
        }

        public void Finish()
        {
            context.Finish();
        }
    }
}
