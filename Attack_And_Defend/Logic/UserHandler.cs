using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using Attack_And_Defend.Models;
using Attack_And_Defend.Data;

namespace Attack_And_Defend.Logic
{
    public class UserHandler
    {
        UserManager<ApplicationUser> userManager;
        ApplicationDbContext context;
        SignInManager<ApplicationUser> signInManager;

        public UserHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager) 
        {
            this.userManager = userManager;
            this.context = context;
            this.signInManager = signInManager;
        }

        public IdentityResult TryCreateUser(string username, string password)
        {
            if (password == null)
                password = "";
            ApplicationUser user = new ApplicationUser();
            user.UserName = username;
            var task = userManager.CreateAsync(user, password);
            context.SaveChanges();
            return task.Result;
        }

        bool checkUserExists(string username)
        {
            return context.Users.Any(u => u.UserName == username);
        }

        public SignInResult TryLogInUser(string username, string password)
        {
            var query = from dbUser in context.ApplicationUsers where dbUser.UserName == username select dbUser;
            ApplicationUser user = query.FirstOrDefault();
            if (user == null)
                return null;
            var result = signInManager.PasswordSignInAsync(user, password, false, false).Result;
            return result;
        }

        public bool TryLogOut()
        {
            var task = signInManager.SignOutAsync();
            task.Wait();
            bool result = task.IsCompletedSuccessfully;
            return result;
        }

        public ApplicationUser GetSignedInUser(ClaimsPrincipal claims)
        {
            if (claims == null)
                return null;
            var result = userManager.GetUserAsync(claims).Result;
            return result;
        }

    }
}
