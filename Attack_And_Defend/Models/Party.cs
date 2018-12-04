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
        private List<Character> characters = new List<Character>();
        public IEnumerable<Character> Characters { get { return characters; } }
        public int IndexLeadCharacter { get; private set; }
        public int IndexCharacterRotatedIn { get; private set; }

        public Party(string name)
        {
            Name = name;
        }

        public Party(int id, string name, List<Character> characters, int indexLeadCharacter)
        {
            Id = id;
            Name = name;
            this.characters = characters;
            IndexLeadCharacter = indexLeadCharacter;
        }

        [Newtonsoft.Json.JsonConstructor]
        public Party(ApplicationUser applicationUser, string name, List<Character> characters, int indexLeadCharacter = 0, int id = 0, int indexCharacterRotatedIn = 0)
        {
            ApplicationUser = applicationUser;
            Name = name;
            this.characters = characters;
            Id = id;
            IndexLeadCharacter = indexLeadCharacter;
            IndexCharacterRotatedIn = indexCharacterRotatedIn;
        }

        public bool TryAddCharacter(Character character)
        {
            if (Characters.Count() >= 5)
            {
                return false;
            }

            characters.Add(character);
            return true;
        }

        public void ChangeLeadCharacter(int index)
        {
            if (index < characters.Count())
            {
                IndexLeadCharacter = index;
                IndexCharacterRotatedIn = IndexLeadCharacter;
            }
        }

        public bool TryRotateCharacter()
        {
            bool foundNonFaintedCharacter = false;
            int indexCharacterChecking = IndexCharacterRotatedIn;
            for (int attempts = 0; attempts < Characters.Count() && !foundNonFaintedCharacter; attempts++)
            {
                indexCharacterChecking = getNextCharacterIndex(IndexCharacterRotatedIn);
                Character characterChecking = characters.ElementAt(indexCharacterChecking);
                if (!characterChecking.Fainted)
                {
                    foundNonFaintedCharacter = true;
                    IndexCharacterRotatedIn = indexCharacterChecking;
                }
            }
            return foundNonFaintedCharacter;
        }

        public Character GetNextCharacter()
        {
            int indexNextCharacter = getNextCharacterIndex(IndexCharacterRotatedIn);
            return characters.ElementAt(indexNextCharacter);
        }

        public Character GetRotatedInCharacter()
        {
            return characters.ElementAt(IndexCharacterRotatedIn);
        }

        int getNextCharacterIndex(int characterIndex)
        {
            int indexNextCharacter;
            if (!(characterIndex == Characters.Count() - 1))
            {
                indexNextCharacter = IndexCharacterRotatedIn + 1;
            }

            else
            {
                indexNextCharacter = 0;
            }
            return indexNextCharacter;
        }
    }
}
