using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Data;
using Attack_And_Defend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Attack_And_Defend.Logic
{
    public class GameDataHandler
    {
        private EFContext context;

        public GameDataHandler(EFContext context)
        {
            this.context = context;
        }

        private Party GetUserSelectedParty(string username)
        {
            var queryUserParties = context.Parties.Include(p => p.Characters).Where(p => p.ApplicationUser.UserName == username).ToList();
            int indexUserParty = context.ApplicationUsers.Where(a => a.UserName == username).Select(a => a.SelectedPartyIndex).FirstOrDefault();
            Party party = queryUserParties.ElementAt(indexUserParty);
            party.SetNextCharacters();
            return party;
        }

        private Party GetCpuParty(int level)
        {
            Party party = context.Parties.Include(p => p.Characters).Where(p => (p.Name == level.ToString()) && p.ApplicationUser == null).FirstOrDefault();
            party.SetNextCharacters();
            return party;
        }

        public void PrepareCombatHandler(CombatHandler combatHandler, string username, int level)
        {
            combatHandler.StartNewGame(username, GetUserSelectedParty(username), GetCpuParty(level));
        }
    }
}
