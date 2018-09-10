using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Party
    {
        public int Id { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }
        public string Name { get; private set; }
        public ICollection<Character> Characters { get; private set; }

        public Character GetLeadCharacter()
        {
            return null;
        }

        public void ChangeLeadCharacter(int index)
        {

        }
    }
}
