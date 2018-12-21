using System;
using System.Collections.Generic;
using System.Text;

namespace Attack_And_Defend.Models
{
    public class GameState
    {
        public Party PlayerParty;
        public Party CpuParty;

        public List<Character> CharactersUsedByPlayer { get; private set; }
        public string UsernameUser { get; private set; }
        public string MessageLog { get; private set; }

        public bool? PlayerWon { get; private set; }
        public int TimesAttacked { get; private set; }
        public int TimesSkillUsed { get; private set; }
        public int CharactersDefeated { get; private set; }
        public int TurnNumber { get; private set; }

        Random random;

        public void Attack()
        {
            Character target = CpuParty.GetRotatedInCharacter();
            Character attacker = PlayerParty.GetRotatedInCharacter();

            int initialHealth = target.RemainingHealth;
            attacker.AttackTarget(target);
            int damage = initialHealth - CpuParty.GetRotatedInCharacter().RemainingHealth;

            MessageLog += attacker.Name + " attacks " + target.Name + " for " + damage + " damage." + Environment.NewLine;
            TimesAttacked++;
            TurnNumber++;

            opponentTurn();
        }

        public void UseSkill()
        {
            Character userSkill = PlayerParty.GetRotatedInCharacter();
            Character targetSkill = CpuParty.GetRotatedInCharacter();

            userSkill.TryUseSkill(targetSkill);
            TimesSkillUsed++;

            opponentTurn();
        }




        void registerAttack(string nameAttacker, string nameTarget, int damage)
        {

        }

        void registerDefeat(string nameAttacker, string nameDefeatedCharacter)
        {
            MessageLog += nameDefeatedCharacter + " was defeated." + Environment.NewLine;
            CharactersDefeated++;
        }

        void RegisterWinOrLoss(bool won)
        {
            if (won)
            {
                MessageLog += UsernameUser + " won the battle." + Environment.NewLine;
            }
            else
            {
                MessageLog += UsernameUser + " lost the battle." + Environment.NewLine;
            }

            PlayerWon = won;
        }



        int getDamageFromPlayerAttack()
        {

        }

        void opponentTurn()
        {
            ensureBothPartiesCanContinue();
            if (PlayerWon == null)
            {
                letOpponentChooseMove();
                ensureBothPartiesCanContinue();
            }
        }

        void ensureBothPartiesCanContinue()
        {
            ensurePartyCanContinue(PlayerParty);
            ensurePartyCanContinue(CpuParty);
        }

        void ensurePartyCanContinue(Party party)
        {
            if (party.GetRotatedInCharacter().Fainted)
            {
                bool partyHasCharacters = party.TryRotateCharacter();
                if (!partyHasCharacters)
                {
                    bool playerWon = (party == CpuParty);
                    RegisterWinOrLoss(playerWon);
                }
            }
        }

        void letOpponentChooseMove()
        {
            CharacterAction decision = getCpuDecision();
            switch (decision)
            {
                case CharacterAction.Attack:
                    CpuParty.GetRotatedInCharacter().AttackTarget(PlayerParty.GetRotatedInCharacter());
                    break;

                case CharacterAction.Skill:
                    CpuParty.GetRotatedInCharacter().TryUseSkill(PlayerParty.GetRotatedInCharacter());
                    break;
            }
        }

        CharacterAction getCpuDecision()
        {
            random = new Random();
            if (CpuParty.GetRotatedInCharacter().CanUseSkill)
            {
                if (random.Next(0, 2) == 1)
                    return CharacterAction.Skill;
            }
            return CharacterAction.Attack;
        }
    }
}
