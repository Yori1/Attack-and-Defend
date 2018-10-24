using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attack_And_Defend.Models;
using Attack_And_Defend.Logic;
using Attack_And_Defend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Attack_And_Defend.Controllers
{
    public class CombatController : Controller
    {
        PartyRepository repository;
        CombatHandler combatHandler;
        CpuPartiesSeed seed = new CpuPartiesSeed();

        string username;


        public CombatController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            repository = new PartyRepository(context);
        }

        public IActionResult LevelSelect()
        {
            return View();
        }

        public IActionResult Combat()
        {
            return View();
        }

        /* public IActionResult Combat(string level)
         {
             Party userParty = repository.GetPartiesUser(username);
             combatHandler = new CombatHandler();
             return View();
    }*/

    }
}