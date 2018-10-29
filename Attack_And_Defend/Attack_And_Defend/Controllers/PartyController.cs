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
            PartyOverviewViewModel vm;
            List<Party> parties = repository.GetPartiesUser(getCurrentUserUsername());
            int selectedPartyIndex = userManager.GetUserAsync(User).Result.SelectedPartyIndex;
            repository.Complete();
            vm = new PartyOverviewViewModel(selectedPartyIndex, parties);
            return View(vm);
        }

        [HttpPost]
        public JsonResult ChangeLeadCharacter(int partyId, int indexNewLeadCharacter)
        {
            var party = getUserParty(partyId);
            party.ChangeLeadCharacter(indexNewLeadCharacter);
            repository.Complete();
            object result = new { success = true };
            return new JsonResult(result);
        }

        Party getUserParty(int partyId)
        { return repository.GetPartiesUser(getCurrentUserUsername()).Where(p => p.Id == partyId).First(); }

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