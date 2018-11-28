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
        List<Character> characters = new List<Character>();
        public IEnumerable<Character> Characters { get { return characters; } }
        public int IndexLeadCharacter { get; private set; }

        public Character ActiveCharacter { get; private set; }

        public Party(string name)
        {
            Name = name;
            updateActiveCharacter(IndexLeadCharacter);
        }

        public Party(int id, string name, List<Character> characters, int indexLeadCharacter)
        {
            Id = id;
            Name = name;
            this.characters = characters;
            IndexLeadCharacter = indexLeadCharacter;
            updateActiveCharacter(IndexLeadCharacter);
        }

        [Newtonsoft.Json.JsonConstructor]
        public Party(ApplicationUser applicationUser, string name, List<Character> characters, int indexLeadCharacter = 0, int id = 0)
        {
            ApplicationUser = applicationUser;
            Name = name;
            this.characters = characters;
            Id = id;
            IndexLeadCharacter = indexLeadCharacter;

            updateActiveCharacter(indexLeadCharacter);
        }

        public bool TryAddCharacter(Character character)
        {
            if (Characters.Count() >= 5)
            {
                return false;
            }

            characters.Add(character);

            if (Characters.Count() == 1)
            {
                updateActiveCharacter(0);
            }
            return true;
        }

        public void ChangeLeadCharacter(int index)
        {
            IndexLeadCharacter = index;
            updateActiveCharacter(IndexLeadCharacter);
        }

        public bool TryRotateCharacter()
        {
            bool foundNonFaintedCharacter = false;
            for (int attempts = 0; attempts < Characters.Count() && !foundNonFaintedCharacter; attempts++)
            {
                ActiveCharacter = GetNextCharacter();
                if (!ActiveCharacter.Fainted)
                {
                    foundNonFaintedCharacter = true;
                }
            }
            return foundNonFaintedCharacter;
        }

        public Character GetNextCharacter()
        {
            int indexActiveCharacter = characters.IndexOf(ActiveCharacter);
            Character nextCharacter;
            if (indexActiveCharacter == Characters.Count() - 1)
            {
                nextCharacter = Characters.ElementAt(indexActiveCharacter);
            }

            else
            {
                nextCharacter = Characters.ElementAt(indexActiveCharacter + 1);
            }

            return nextCharacter;
        }

        void updateActiveCharacter(int index)
        {
            if(characters.Count()>0)
             ActiveCharacter = Characters.ElementAt(index);
        }
    }
}
