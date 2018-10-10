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

        bool TryAddCharacter(Character character, int idPartyToAddTo);

        Dictionary<JobNumber, int> GetAmountForEveryJob();

        void Complete();
    }
}
