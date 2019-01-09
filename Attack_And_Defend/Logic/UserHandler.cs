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
        UserRepository repository;
        SignInManager<ApplicationUser> signInManager;

        public UserHandler(UserApplicationContext userApplicationContext, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) 
        {
            repository = new UserRepository(userApplicationContext);
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IdentityResult TryCreateUser(string username, string password)
        {
            if (password == null)
                password = "";
            ApplicationUser user = new ApplicationUser();
            user.UserName = username;
            var task = userManager.CreateAsync(user, password);
            return task.Result;
        }


        public bool CheckUserExists(string username)
        {
            return repository.CheckUserExists(username);
        }

        public SignInResult TryLogInUser(string username, string password)
        {
            var query = from dbUser in repository.GetAllUsers() where dbUser.UserName == username select dbUser;
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

        public ApplicationUser GetApplicationUser(ClaimsPrincipal principal)
        {
            if (principal == null)
                return null;
            var result = userManager.GetUserAsync(principal).Result;
            return result;
        }

    }
}
