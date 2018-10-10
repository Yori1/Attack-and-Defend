using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Attack_And_Defend.Models;
using Attack_And_Defend.Data;

namespace Attack_And_Defend.Data
{
    public class PartyRepository
    {
        ITestableContext context;

        public PartyRepository()
        {
            context = new MemoryContext();
        }

        public PartyRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Party> GetPartiesUser(string username)
        {
            return context.GetPartiesUser(username);
        }

        public bool TryAddCharacter(Character character, int idPartyToAddTo)
        {
            return context.TryAddCharacter(character, idPartyToAddTo);
        }
        public bool TryAddParty(string name, string username)
        {
            return context.TryAddParty(name, username);
        }

        public Dictionary<JobNumber, int> GetAmountForEveryJob()
        {
            return context.GetAmountForEveryJob();
        }

        public void Complete()
        {
            context.Complete();
        }
    }
}
