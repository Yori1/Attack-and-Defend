using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Attack_And_Defend.Models;

namespace Attack_And_Defend.Data
{
    public class CpuPartiesSeed
    {
        List<Party> parties = new List<Party>();

        public CpuPartiesSeed()
        {
            List<Character> charactersParty1 = new List<Character>()
            {
                Character.GetConcreteCharacter("Enemy", 2,2,2,2,JobNumber.Hunter),
                Character.GetConcreteCharacter("Enemy", 2,2,2,2,JobNumber.Hunter),
                Character.GetConcreteCharacter("Enemy", 2,2,2,2,JobNumber.Hunter)
            };
            parties.Add(new Party(null, "1", charactersParty1));
        }

        public Party GetCPUPartyByLevel(string level)
        {
            return parties.Where(p => p.Name == level).First();
        }
    }
}
