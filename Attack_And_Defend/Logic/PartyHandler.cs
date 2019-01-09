using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Attack_And_Defend.Models;
using Attack_And_Defend.Data;

namespace Attack_And_Defend.Logic
{
    public class PartyHandler
    {
        PartyRepository repository;
        UserManager<ApplicationUser> userManager;

        public PartyHandler(PartyApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            this.repository = new PartyRepository(context);
            this.userManager = userManager;
        }

        public List<Party> GetPartiesUser(string username)
        {
            return repository.GetPartiesUser(username);
        }

        public bool TryAddCharacter(Character character, int idPartyToAddTo)
        {
            bool result = repository.TryAddCharacter(character, idPartyToAddTo);
            repository.Finish();
            return result;
        }
        public bool TryAddParty(string name, ClaimsPrincipal principal)
        {
            bool addedParty = repository.TryAddParty(name, getCurrentUserUsername(principal));
            repository.Finish();
            return addedParty;
        }

        public Dictionary<JobNumber, int> GetAmountForEveryJob()
        {
            return repository.GetAmountForEveryJob();
        }

        public void ChangeLeadIndexParty(int partyId, int indexNewLeadCharacter)
        {
            repository.ChangeLeadIndexParty(indexNewLeadCharacter, partyId);
            repository.Finish();
        }

        public PartyOverviewViewModel GetPartyOverviewViewModel(ClaimsPrincipal principal)
        {
            PartyOverviewViewModel vm;
            string username = getCurrentUserUsername(principal);
            List<Party> parties = repository.GetPartiesUser(username);
            var task = userManager.GetUserAsync(principal);
            task.Wait();
            int selectedIndex = task.Result.SelectedPartyIndex;
            vm = new PartyOverviewViewModel(selectedIndex, parties);
            return vm;
        }

        public void ChangeActiveParty(int partyIndex, ClaimsPrincipal claimsPrincipal)
        {
            string username = getCurrentUserUsername(claimsPrincipal);
            repository.ChangeActiveParty(username, partyIndex);
            repository.Finish();
        }

        string getCurrentUserUsername(ClaimsPrincipal principal)
        {
            var user = userManager.GetUserAsync(principal).Result;
            if (user == null)
                return null;
            else
                return user.UserName;
        }
    }
}
