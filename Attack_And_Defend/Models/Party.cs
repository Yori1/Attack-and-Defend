using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Party
    {
        //for EF:
        public int Id { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }
        public string Name { get; private set; }
        public List<Character> Characters { get; private set; } = new List<Character>();
        int leadCharacterIndex;

        public Character ActiveCharacter;

        int CharactersTried = 0;

        public Party(string name)
        {
            Name = name;
        }

        public Party(ApplicationUser user, string name, List<Character> characters)
        {
            ApplicationUser = user;
            Name = name;
            Characters = characters;
        }

        public bool TryAddCharacter(Character character)
        {
            if (Characters.Count() >= 5)
            {
                return false;
            }
            else
            {
                Characters.Add(character);
                return true;
            }
        }

        public bool TryRotateActiveCharacter()
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
                CharactersTried += 1;
                if (CharactersTried != Characters.Count())
                {
                    TryRotateActiveCharacter();
                }
                else
                {
                    CharactersTried = 0;
                    return false;
                }
                return true;

            }
            else
                return true;

        }

        public void ChangeLeadCharacter(int index)
        {
            leadCharacterIndex = index;
        }
    }
}
