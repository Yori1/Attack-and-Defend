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
    {/*
        EFContext context;
        UserManager<ApplicationUser> userManager;

        public CombatController(UserManager<ApplicationUser> userManager, EFContext context)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult LevelSelect()
        {
            return View();
        }


        public IActionResult Combat(int level)
        {
            /CombatHandler combatHandler = createNewCombatHandler(level);
            string json = JsonHandler.ToJson(combatHandler);
            HttpContext.Session.SetString("CombatHandler", json);
            return View(combatHandler);
        }
        

        public IActionResult PlayerDecision(CharacterAction action)
        {
            string jsonBeforeAction = HttpContext.Session.GetString("modifiedCombatHandler");
            if(jsonBeforeAction == null)
                jsonBeforeAction = HttpContext.Session.GetString("CombatHandler");
            CombatHandler combatHandler = JsonHandler.FromJson(jsonBeforeAction);
            choosePlayerAction(action, combatHandler);
            string jsonAfterAction = JsonHandler.ToJson(combatHandler);
            HttpContext.Session.SetString("modifiedCombatHandler", jsonAfterAction);
            return View("Views/Combat/Combat.cshtml", combatHandler);
        }
        
        void choosePlayerAction(CharacterAction action, CombatHandler handler)
        {
            switch(action)
            {
                case CharacterAction.Attack:
                    handler.Attack();
                    break;

                case CharacterAction.Skill:
                    handler.UseSkill();
                    break;
            }
        }


        CombatHandler createNewCombatHandler(int cpuLevel)
        {
            string username = userManager.GetUserAsync(User).Result.UserName;
            Party userParty = context.GetActiveParty(username);
            Party cpuParty = context.GetCpuParty(cpuLevel.ToString());

            var combatHandler = new CombatHandler(userParty, cpuParty,0, username);
            return combatHandler;
        }
        
    */}
}