using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Attack_And_Defend.Models;
using Attack_And_Defend.Logic;
using Attack_And_Defend.Data;

namespace Attack_And_Defend.Controllers
{
    public class PartyController : Controller
    {
        UserManager<ApplicationUser> userManager;
        PartyRepository repository;

        public PartyController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.repository = new PartyRepository(context);
        }

        string getCurrentUserUsername()
        {
            return userManager.GetUserAsync(User).Result.UserName;
        }

        public IActionResult PartyOverview()
        {
            List<Party> model = repository.GetPartiesUser(getCurrentUserUsername());
            repository.Complete();
            return View(model);
        }

        public IActionResult AddParty(string partyName)
        {
            repository.TryAddParty(partyName, getCurrentUserUsername());
            repository.Complete();
            return RedirectToAction("PartyOverview");
        }

        public IActionResult AddCharacter(int partyIdToAddTo)
        {
            return View("Views/Party/CharacterDetail.cshtml", new CharacterDetailsViewModel(partyIdToAddTo));
        }

        [HttpPost]
        public IActionResult SaveCharacter(string name, int attack, int magicDefense, int physicalDefense, int health, JobNumber jobNumber, int partyId)
        {
            Character character = Character.GetConcreteCharacter(name, attack, magicDefense, physicalDefense, health, jobNumber);
            repository.TryAddCharacter(character, partyId);
            repository.Complete();
            return RedirectToAction("PartyOverview");
        }
    }
}