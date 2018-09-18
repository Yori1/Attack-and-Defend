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
        UserManager<ApplicationUser> userManager;
        ApplicationDbContext context;
        SignInManager<ApplicationUser> signInManager;

        public UserHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.signInManager = signInManager;

            seedSampleUser();
        }

        void seedSampleUser()
        {
            string name = "SampleUser";
            if (!context.Users.Any(u => u.UserName == name))
            {
                TryCreateUser(name, "Password1!");
                context.SaveChanges();

                var query = from sampleuser in context.ApplicationUsers where sampleuser.UserName == "SampleUser" select sampleuser;
                ApplicationUser sampleUser = query.First();
                var party = new Party("Party");

                for (int x = 0; x < 5; x++)
                    party.Characters.Add(new Character("testChar" + (x+1), 2, 2, 2, true));

                sampleUser.Parties.Add(party);

                context.SaveChanges();
            }
        }

       public bool TryCreateUser(string username, string password)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = username;
            var task = userManager.CreateAsync(user, password);
            context.SaveChanges();
            return task.Result.Succeeded;
        }

        public bool CheckUserExists(string username)
        {
            return context.Users.Any(u => u.UserName == username);
        }

        public async Task<bool> TryLogInUser(string username, string password, ClaimsPrincipal userClaims)
        {
            var query = from dbUser in context.ApplicationUsers where dbUser.UserName == username select dbUser;
            ApplicationUser user = query.ToList()[0];
            if (user == null)
                return false;
            var task = await signInManager.PasswordSignInAsync(user, password, false, false);
            return task.Succeeded;
        }

        public bool TryLogOut()
        {
            var task = signInManager.SignOutAsync();
            task.Wait();
            bool result = task.IsCompletedSuccessfully;
            return result;
        }

        public ApplicationUser GetSignedInUser(ClaimsPrincipal user)
        {
            if (user == null)
                return null;
           var result = userManager.GetUserAsync(user).Result;
            return result;
        }
    }
}
