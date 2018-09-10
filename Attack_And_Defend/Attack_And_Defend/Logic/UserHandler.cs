using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attack_And_Defend.Models;
using Attack_And_Defend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Attack_And_Defend.Logic
{
    public class UserHandler
    {
        ClaimsPrincipal user;
        UserManager<ApplicationUser> userManager;
        ApplicationDbContext context;
        SignInManager<ApplicationUser> signInManager;

        public UserHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager, ClaimsPrincipal user)
        {
            this.userManager = userManager;
            this.context = context;
            this.signInManager = signInManager;
            this.user = user;
        }

        public bool TryCreateUser(string username, string password)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = username;
            var task = userManager.CreateAsync(user, password);
            context.SaveChanges();
            return task.Result.Succeeded;
        }

        public bool TryLogInUser(string username, string password)
        {
            var query = from dbUser in context.ApplicationUsers where dbUser.UserName == username select dbUser;
            ApplicationUser user = query.ToList()[0];
            if (user == null)
                return false;
            var result = signInManager.PasswordSignInAsync(user, password, false, false);
            return (result.IsCompletedSuccessfully == true);
        }

        public ApplicationUser GetSignedInUser()
        {
            if (user == null)
                return null;
           var task = userManager.GetUserAsync(user);
            if (task.IsCompletedSuccessfully)
                return task.Result;
            else
                return null;
        }
    }
}
