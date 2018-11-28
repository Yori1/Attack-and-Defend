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
        ApplicationDbContext context;
        PartyRepository repository;

        public PartyController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.repository = new PartyRepository(context);
            this.context = context;
        }

        string getCurrentUserUsername()
        {
            var user = userManager.GetUserAsync(User).Result;
            if (user == null)
                return null;
            else
                return user.UserName;
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
            repository.ChangeLeadIndexParty(partyId, indexNewLeadCharacter);
            repository.Complete();
            return new JsonResult(new { success = true });
        }

        [HttpPost]
        public JsonResult ChangeActiveParty(int partyIndex)
        {
            string u = getCurrentUserUsername();
            context.ChangeActiveParty(partyIndex, getCurrentUserUsername());
            context.SaveChanges();
            return new JsonResult(new { success = true });
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
            repository.TryAddCharacter(name, attack, magicDefense, physicalDefense, health, jobNumber, partyId);
            repository.Complete();
            return RedirectToAction("PartyOverview");
        }


    }
}