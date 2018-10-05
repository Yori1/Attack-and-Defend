using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using Attack_And_Defend.Logic;
using Attack_And_Defend.Models;

namespace Attack_And_Defend.Controllers
{
    public class HomeController : Controller
    {
        UserHandler userHandler;

        public HomeController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, Data.ApplicationDbContext context)
        {
             userHandler = new UserHandler(userManager, context, signInManager);
        }

        public IActionResult Index()
        {
            if (userHandler.GetSignedInUser(User) == null)
                return View("Views/Home/NotLoggedIn.cshtml");
            else
                return RedirectToAction("PartyOverview", "Party");
        }

        [HttpPost]
        public IActionResult Register(string UsernameRegister, string PasswordRegister, string PasswordConfirm)
        {
            if (PasswordRegister != PasswordConfirm)
                return Index();
            userHandler.TryCreateUser(UsernameRegister, PasswordRegister);
            return Index();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string UsernameLogin, string PasswordLogin)
        {
            bool loggedIn = await userHandler.TryLogInUser(UsernameLogin, PasswordLogin, User);
            if(loggedIn)
                return RedirectToAction("PartyOverview", "Party");
            else
                return View("Views/Home/NotLoggedIn.cshtml");
        }

        public IActionResult LogOut()
        {
            bool logoutSuccesful = userHandler.TryLogOut();
            if (logoutSuccesful)
                return View("Views/Home/NotLoggedIn.cshtml");
            return Index();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
