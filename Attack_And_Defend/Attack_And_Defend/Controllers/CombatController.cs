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
using Newtonsoft.Json;


namespace Attack_And_Defend.Controllers
{
    public class CombatController : Controller
    {
        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;

        public CombatController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult LevelSelect()
        {
            return View();
        }

        public JsonResult GetCombatHandler()
        {
            return new JsonResult(getCombatHandlerFromSession());
        }

        public IActionResult Combat(int level)
        {
            CombatHandler combatHandler = createNewCombatHandler(level);
            HttpContext.Session.SetString("combatHandler", JsonConvert.SerializeObject(combatHandler));
            return View(combatHandler);
        }
        
        CombatHandler getCombatHandlerFromSession()
        {
            string json = HttpContext.Session.GetString("CombatHandler");
            CombatHandler combatHandler = JsonConvert.DeserializeObject<CombatHandler>(json);
            return combatHandler;
        }

        CombatHandler createNewCombatHandler(int cpuLevel)
        {
            CpuPartiesSeed seed = new CpuPartiesSeed();
            string username = userManager.GetUserAsync(User).Result.UserName;
            Party userParty = context.GetActiveParty(username);
            Party cpuParty = seed.GetCPUPartyByLevel(cpuLevel.ToString());
            var combatHandler = new CombatHandler(userParty, cpuParty);
            return combatHandler;
        }
        
        void saveCombatHandlerInSession(CombatHandler combatHandler)
        {
            
        }
    }
}