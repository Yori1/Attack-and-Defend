using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attack_And_Defend.Models;
using Attack_And_Defend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace Attack_And_Defend.Data
{
    public class PartyHandler
    {
        UserManager<ApplicationUser> userManager;
        ApplicationDbContext context;
        SignInManager<ApplicationUser> signInManager;

        public PartyHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.signInManager = signInManager;
        }

        ApplicationUser GetUserByClaims(ClaimsPrincipal user)
        {
            return userManager.GetUserAsync(user).Result;
        }

        public List<Party> GetPartiesUser(ClaimsPrincipal user)
        {
            var userID = GetUserByClaims(user).Id;
            var query = from party in context.Parties where party.ApplicationUser.Id == userID select party;
            var partylist = query.ToList();

            foreach(Party party in partylist)
            {
                getCharactersFromDB(party.Id);
            }

            return partylist;
        }

        void getCharactersFromDB(int partyID)
        {
            var query = from character in context.Characters where character.Party.Id == partyID select character;
            var list = query.ToList();
        }

        public bool TryAddParty(string name, ClaimsPrincipal userClaims)
        {
            string nameToUse = name;
            if (name == "")
                return false;
            var query = from party in context.Parties where party.Name == nameToUse select party;
            if (query.Count() > 0)
                return false;
            string userName = GetUserByClaims(userClaims).UserName;
            Party partyToAdd = new Party(name);
            context.Parties.Add(partyToAdd);
            return true;
        }


    }
}
