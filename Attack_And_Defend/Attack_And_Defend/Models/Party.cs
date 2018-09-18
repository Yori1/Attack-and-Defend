using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Party
    {
        public ApplicationUser ApplicationUser { get; private set; }
        public string Name { get; private set; }
        public ICollection<Character> Characters { get; private set; } = new List<Character>();


        //for EF:
        public int Id { get; private set; }

        public Party(string name)
        {
            Name = name;
        }

        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }

        public Character GetLeadCharacter()
        {
            return null;
        }

        public void ChangeLeadCharacter(int index)
        {

        }
    }
}
