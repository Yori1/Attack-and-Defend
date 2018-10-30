using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Attack_And_Defend.Models;
using Attack_And_Defend.Logic;
using Attack_And_Defend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Attack_And_Defend.Controllers
{
    public class CombatController : Controller
    {
        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;
        CombatHandler combatHandler;

        public CombatController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult LevelSelect()
        {
            return View();
        }

        public IActionResult Combat()
        {
            return View();
        }

        public IActionResult Combat(string level)
        {
            return View();
        }
        /*
        CombatHandler getCombatHandlerFromSession()
        {
            string json = HttpContext.Session.GetString("CombatHandler");
            if (json == null)
            {

            }
        }

        CombatHandler createNewCombatHandler(int cpuLevel)
        {
            CpuPartiesSeed seed = new CpuPartiesSeed();
            string username = userManager.GetUserAsync(User).Result.UserName;
            Party userParty = context.GetActiveParty(username);
            Party cpuParty = seed.GetCPUPartyByLevel(cpuLevel.ToString());
            var combatHandler = new CombatHandler(userParty, cpuParty);
        }
        */
        void saveCombatHandlerInSession(CombatHandler combatHandler)
        {
            
        }
    }
}