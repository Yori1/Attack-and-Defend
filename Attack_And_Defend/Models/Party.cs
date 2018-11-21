using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attack_And_Defend.Models
{
    public class Party
    {
        //for EF:
        public int Id { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }
        public string Name { get; private set; }
        public List<Character> Characters { get; private set; } = new List<Character>();
        public int IndexLeadCharacter { get; private set; }

        public Character ActiveCharacter
        {
            get
            {
                if (Characters.Count() == 0)
                    return null;
                return Characters[IndexLeadCharacter];
            }
        }

        public Party(string name)
        {
            Name = name;
        }

        [Newtonsoft.Json.JsonConstructor]
        public Party(ApplicationUser applicationUser, string name, List<Character> characters, int indexLeadCharacter = 0, int id = 0)
        {
            ApplicationUser = applicationUser;
            Name = name;
            Characters = characters;
            Id = id;
            IndexLeadCharacter = indexLeadCharacter;
        }

        public bool TryAddCharacter(Character character)
        {
            if (Characters.Count() >= 5)
            {
                return false;
            }
            Characters.Add(character);
            if (Characters.Count() == 1)
            {
                ChangeLeadCharacter(0);
            }
            return true;
        }

        bool findNonFaintedCharacter(ref int charactersTried)
        {
            int indexCurrentlyActiveCharacter = Characters.IndexOf(ActiveCharacter);
            if (indexCurrentlyActiveCharacter == Characters.Count() - 1)
            {
                ActiveCharacter = Characters[0];
            }

            else
            {
                ActiveCharacter = Characters[indexCurrentlyActiveCharacter + 1];
            }

            if (ActiveCharacter.Fainted)
            {
                charactersTried += 1;
                if (charactersTried != Characters.Count())
                {
                    TryRotateActiveCharacter();
                }
                else
                {
                    charactersTried = 0;
                    return false;
                }
                return true;

            }
            else
                return true;

        }

        public bool TryRotateActiveCharacter()
        {
            int charactersTried = 0;
            return true;

        }

        public void ChangeLeadCharacter(int index)
        {
            IndexLeadCharacter = index;
        }
    }
}
