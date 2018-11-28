using System;
using System.Collections.Generic;
using System.Text;

namespace Attack_And_Defend.Models
{
    public class Game
    {
        public List<Character> CharactersUsedByPlayer { get; private set; }
        public string UsernameUser { get; private set; }
        public string MessageLog { get; private set; }

        public bool? Won { get; private set; }
        public int TimesAttacked { get; private set; }
        public int TimesSkillUsed { get; private set; }
        public int CharactersDefeated { get; private set; }

        public Game(string username)
        {
            UsernameUser = username;
        }

        public void RegisterAttack(string nameAttacker, string nameTarget, int damage)
        {
            MessageLog += nameAttacker + " attacks " + nameTarget + " for " + damage + " damage." + Environment.NewLine;
            TimesAttacked++;
        }

        public void RegisterDefeat(string nameAttacker, string nameDefeatedCharacter)
        {
            MessageLog += nameDefeatedCharacter + " was defeated." + Environment.NewLine;
            CharactersDefeated++;
        }

        public void RegisterUsedSkill(Character userSkill, Character target)
        {
        }

        public void RegisterWinOrLoss(bool won)
        {
            if (won)
            {
                MessageLog += UsernameUser + " won the battle." + Environment.NewLine;
            }
            else
            {
                MessageLog += UsernameUser + " lost the battle." + Environment.NewLine;
            }

            Won = won;
        }



    }
}
