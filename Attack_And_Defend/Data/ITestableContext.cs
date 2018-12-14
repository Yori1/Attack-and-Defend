using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Attack_And_Defend.Models;

namespace Attack_And_Defend.Data
{
    public interface ITestableContext
    {
        List<Party> GetPartiesUser(string userId);

        bool TryAddParty(string name, string username);

        bool TryAddCharacter(string name, int attack, int magicDefense, int physicalDefense, int health, JobNumber jobNumber, int partyId);

        Dictionary<JobNumber, int> GetAmountForEveryJob();

        void Complete();
    }
}
