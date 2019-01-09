using System;
using System.Collections.Generic;
using System.Text;

namespace Attack_And_Defend.Models
{
    public class Game
    {
        public List<Character> CharactersUsedByPlayer { get; private set; }
        public string UsernameUser { get; set; }
        public List<string> MessageLog { get; private set; } = new List<string>();

        public bool? Won { get; private set; }
        public int TimesAttacked { get; private set; }
        public int TimesSkillUsed { get; private set; }
        public int CharactersDefeated { get; private set; }

        public Game()
        {
        }

        public void RegisterAttack(string nameAttacker, string nameTarget, int damageDealt)
        {
            MessageLog.Add(nameAttacker + " attacks " + nameTarget + " for " + damageDealt + " damage." );
            TimesAttacked++;
        }

        public void RegisterDefeat(Character defeatedCharacter)
        {
            MessageLog.Add(defeatedCharacter.Name + " was defeated." );
            if(!defeatedCharacter.NextCharacterInParty.Fainted)
              MessageLog.Add(defeatedCharacter.NextCharacterInParty.Name + " takes " + defeatedCharacter.Name + "'s place.");

            MessageLog.Add("");
            CharactersDefeated++;
        }

        public void RegisterUsedSkill(Character userSkill, Character target)
        {
            MessageLog.Add(userSkill.Name + " used the " + userSkill.JobNumber.ToString() + " class' unique skill on " + target.Name + ".");
        }

        public void RegisterWinOrLoss(bool won)
        {
            if (won)
            {
                MessageLog.Add("User " + UsernameUser + " won the battle." );
            }
            else
            {
                MessageLog.Add("User " + UsernameUser + " lost the battle." + Environment.NewLine);
            }

            Won = won;
        }



    }
}
