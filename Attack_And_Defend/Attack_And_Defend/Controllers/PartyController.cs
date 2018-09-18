using System;
using System.Collections.Generic;
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
    public class PartyController : Controller
    {
        PartyHandler partyHandler;

        public PartyController(UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            partyHandler = new PartyHandler(userManager, context, signInManager);
        }

        public IActionResult PartyOverview()
        {
            List<Party> model = partyHandler.GetPartiesUser(User);
            return View(model);
        }
    }
}