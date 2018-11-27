using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Models;

namespace Attack_And_Defend.Logic
{
    public class CombatHandler
    {
        public string MessageLog { get { return game.MessageLog; } }
        public bool? PlayerWon { get { return game.Won; } }

        public Party PlayerParty;
        public Party CpuParty;

        Game game;

        Random random;

        public CombatHandler(Party playerParty, Party cpuParty, string username)
        {
            this.PlayerParty = playerParty;
            this.CpuParty = cpuParty;
            this.game = new Game(username);
        }

        public void Attack()
        {
            int damage = getDamageFromPlayerAttack();
            game.RegisterAttack(PlayerParty.ActiveCharacter.Name, CpuParty.ActiveCharacter.Name, damage);
            opponentTurn();
        }

        public void UseSkill()
        {
            PlayerParty.ActiveCharacter.TryUseSkill(CpuParty.ActiveCharacter);
            opponentTurn();
        }

        int getDamageFromPlayerAttack()
        {
            int initialHealth = CpuParty.ActiveCharacter.RemainingHealth;
            PlayerParty.ActiveCharacter.AttackTarget(CpuParty.ActiveCharacter);
            return initialHealth - CpuParty.ActiveCharacter.RemainingHealth;
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

        void letOpponentChooseMove()
        {
            CharacterAction decision = getCpuDecision();
            switch(decision)
            {
                case CharacterAction.Attack:
                    CpuParty.ActiveCharacter.AttackTarget(PlayerParty.ActiveCharacter);
                    break;

                case CharacterAction.Skill:
                    CpuParty.ActiveCharacter.TryUseSkill(PlayerParty.ActiveCharacter);
                    break;
            }
        }

        void ensureBothPartiesCanContinue()
        {
            ensurePartyCanContinue(PlayerParty);
            ensurePartyCanContinue(CpuParty);
        }

        void ensurePartyCanContinue(Party party)
        {
            if (party.ActiveCharacter.Fainted)
            {
                bool partyHasCharacters = party.TryRotateCharacter();
                if (!partyHasCharacters)
                {
                    bool playerWon = (party == CpuParty);
                    game.RegisterWinOrLoss(playerWon);
                }
            }
        }

        CharacterAction getCpuDecision()
        {
            random = new Random();
            if (CpuParty.ActiveCharacter.CanUseSkill)
            {
                if (random.Next(0, 2) == 1)
                    return CharacterAction.Skill;
            }
            return CharacterAction.Attack;
        }

    }

}
