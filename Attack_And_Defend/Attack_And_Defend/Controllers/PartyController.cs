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
        PartyHandler partyHandler;
        string context;

        public PartyController(PartyHandler partyHandler)
        {
            this.partyHandler = partyHandler;
        }

        public IActionResult PartyOverview()
        {
            return View(partyHandler.GetPartyOverviewViewModel(User));
        }

        [HttpPost]
        public JsonResult ChangeLeadCharacter(int partyId, int indexNewLeadCharacter)
        {
            partyHandler.ChangeLeadIndexParty(partyId, indexNewLeadCharacter);
            return new JsonResult(new { success = true });
        }

        [HttpPost]
        public JsonResult ChangeActiveParty(int partyIndex)
        {
            partyHandler.ChangeActiveParty(partyIndex, User);
            return new JsonResult(new { success = true });
        }


        public IActionResult AddParty(string partyName)
        {
            partyHandler.TryAddParty(partyName, User);
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
            partyHandler.TryAddCharacter(character, partyId);
            return RedirectToAction("PartyOverview");
        }
    }
}