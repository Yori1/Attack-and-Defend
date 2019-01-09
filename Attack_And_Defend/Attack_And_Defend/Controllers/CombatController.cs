using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Attack_And_Defend.Models;
using Attack_And_Defend.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Attack_And_Defend.Controllers
{
    public class CombatController : Controller
    {
        CombatHandler combatHandler;
        GameDataHandler gameDataHandler;
        UserHandler userHandler;

        public CombatController(CombatHandler combatHandler, GameDataHandler gameDataHandler, UserHandler userHandler)
        {
            this.combatHandler = combatHandler;
            this.gameDataHandler = gameDataHandler;
            this.userHandler = userHandler;
        }

        public IActionResult LevelSelect()
        {
            return View();
        }


        public IActionResult Combat(int level)
        {
            string username = userHandler.GetApplicationUser(User).UserName;
            gameDataHandler.PrepareCombatHandler(combatHandler, username, level);
            return View(combatHandler);
        }
        

        public IActionResult PlayerDecision(CharacterAction action)
        {
            choosePlayerAction(action, combatHandler);
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
        
    }
}