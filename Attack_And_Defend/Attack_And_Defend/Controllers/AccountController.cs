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
    public class AccountController : Controller
    {
        UserHandler userHandler;

        public AccountController(UserHandler userHandler)
        {
            this.userHandler = userHandler;
        }

        public IActionResult Index()
        {
            if (userHandler.GetApplicationUser(User) == null)
                return View("Views/Account/NotLoggedIn.cshtml", new LoginViewModel(""));
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
                return View("Views/Account/NotLoggedIn.cshtml", vm);
            }
            var result = userHandler.TryCreateUser(UsernameRegister, PasswordRegister);
            if(result.Succeeded)
            {
                return RedirectToAction("Login", "Account", new RouteValueDictionary(
                    new { UsernameLogin = UsernameRegister, PasswordLogin = PasswordRegister }) );
            }
            else
            {
                vm = new LoginViewModel(result.Errors);
                return View("Views/Account/NotLoggedIn.cshtml", vm);
            }
        }

        public async Task<IActionResult> Login(string UsernameLogin, string PasswordLogin)
        {
            var result = userHandler.TryLogInUser(UsernameLogin, PasswordLogin);
            if (result.Succeeded)
                return RedirectToAction("PartyOverview", "Party");
            else
            {
                LoginViewModel vm = new LoginViewModel("Failed login, make sure you registered using this password.");
                return View("Views/Account/NotLoggedIn.cshtml", vm);
            }
        }

        public IActionResult LogOut()
        {
            bool logoutSuccesful = userHandler.TryLogOut();
            if (logoutSuccesful)
                return View("Views/Account/NotLoggedIn.cshtml", new LoginViewModel(""));
            return Index();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
