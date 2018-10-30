using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Microsoft.AspNetCore.Owin;
using System.Security.Claims;

using Attack_And_Defend.Logic;
using Attack_And_Defend.Models;
using Microsoft.AspNetCore.Routing;

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
                return View("Views/Home/NotLoggedIn.cshtml", new LoginViewModel(""));
            else
                return RedirectToAction("PartyOverview", "Party");
        }

        [HttpPost]
        public IActionResult Register(string UsernameRegister, string PasswordRegister, string PasswordConfirm)
        {

            LoginViewModel vm;
            if (PasswordRegister != PasswordConfirm)
            {
                vm = new LoginViewModel("Passwords are not identical.");
                return View("Views/Home/NotLoggedIn.cshtml", vm);
            }
            var result = userHandler.TryCreateUser(UsernameRegister, PasswordRegister);
            if(result.Succeeded)
            {
                return RedirectToAction("Login", "Home", new RouteValueDictionary(
                    new { UsernameLogin = UsernameRegister, PasswordLogin = PasswordRegister }) );
            }
            else
            {
                vm = new LoginViewModel(result.Errors);
                return View("Views/Home/NotLoggedIn.cshtml", vm);
            }
        }

        public async Task<IActionResult> Login(string UsernameLogin, string PasswordLogin)
        {
            var result = userHandler.TryLogInUser(UsernameLogin, PasswordLogin, User);
            if (result.Succeeded)
                return RedirectToAction("PartyOverview", "Party");
            else
            {
                LoginViewModel vm = new LoginViewModel("Failed login, make sure you registered using this password.");
                return View("Views/Home/NotLoggedIn.cshtml", vm);
            }
        }

        public IActionResult LogOut()
        {
            bool logoutSuccesful = userHandler.TryLogOut();
            if (logoutSuccesful)
                return View("Views/Home/NotLoggedIn.cshtml", new LoginViewModel("Views/Home/NotLoggedIn.cshtml"));
            return Index();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
