using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Attack_And_Defend.Models;
using Attack_And_Defend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Attack_And_Defend.Logic;

namespace Attack_And_Defend.Controllers
{
    public class HomeController : Controller
    {
        UserHandler userHandler;

        public HomeController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            userHandler = new UserHandler(userManager, context, signInManager, User);
        }

        public IActionResult Index()
        {
            if (userHandler.GetSignedInUser() == null)
                return View("Views/Home/NotLoggedIn.cshtml");
            else
                return View();
        }

        [HttpPost]
        public IActionResult Index(string Username, string Password)
        {
            if (userHandler.TryCreateUser(Username, Password))
                return View();
 
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
