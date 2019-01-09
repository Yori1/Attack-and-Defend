using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Attack_And_Defend.Models;
using System.Security.Claims;

namespace Attack_And_Defend.Data
{
    public interface IUserContext
    {

        bool CheckUserExists(string username);

        IEnumerable<ApplicationUser> GetAllUsers();

        void Finish();

    }
}
