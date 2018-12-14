using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Attack_And_Defend.Models;
using Attack_And_Defend.Data;

namespace Attack_And_Defend.Data
{
    public class PartyRepository
    {
        ApplicationDbContext applicationContext;
        ITestableContext context;

        public PartyRepository()
        {
            context = new MemoryContext();
        }

        public PartyRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.applicationContext = context;
        }

        public List<Party> GetPartiesUser(string username)
        {
            return context.GetPartiesUser(username);
        }

        public bool TryAddCharacter(string name, int attack, int magicDefense, int physicalDefense, int health, JobNumber jobNumber, int partyId)
        {
            return context.TryAddCharacter(name, attack, magicDefense, physicalDefense, health, jobNumber, partyId);
        }

        public bool TryAddParty(string name, string username)
        {
            return context.TryAddParty(name, username);
        }

        public Dictionary<JobNumber, int> GetAmountForEveryJob()
        {
            return context.GetAmountForEveryJob();
        }

        public void ChangeLeadIndexParty(int partyId, int indexNewLeadCharacter)
        {
            var party = applicationContext.Parties.Where(p=>p.Id == partyId).First();
            party.ChangeLeadCharacter(indexNewLeadCharacter);
            applicationContext.Entry(party).State = EntityState.Modified;
            context.Complete();
        }

        public void Complete()
        {
            context.Complete();
        }

        static Character getImplementation(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, Party party, JobNumber jobNumber)
        {
            Character character = null;
            switch (jobNumber)
            {
                case JobNumber.Hunter:
                    character = new Hunter(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth, party);
                    break;

                case JobNumber.Mage:
                    character = new Hunter(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth, party);
                    break;
            }

            return character;
        }
    }
}
