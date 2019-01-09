using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Models;

namespace Attack_And_Defend.Logic
{
    public class CombatHandler
    {
        public List<string> MessageLog { get { return game.MessageLog; } }
        public bool? PlayerWon { get { return game.Won; } }
        public int TurnNumber { get; private set; }

        public Party PlayerParty { get; private set; }
        public Party CpuParty { get; private set; }

        Game game;

        Random random;

        public CombatHandler()
        {
        }

        public void StartNewGame(string username, Party playerParty, Party cpuParty)
        {
            game = new Game();
            game.UsernameUser = username;
            this.PlayerParty = playerParty;
            this.CpuParty = cpuParty;

            TurnNumber = 0;
        }


        public void Attack()
        {
            if(PlayerWon == null)
            {
                int damage = getDamageFromPlayerAttack();
                game.RegisterAttack(PlayerParty.GetRotatedInCharacter().Name, CpuParty.GetRotatedInCharacter().Name, damage);
                opponentTurn();
                TurnNumber++;
            }
        }

        public void UseSkill()
        {
            if(PlayerWon == null)
            {
                PlayerParty.GetRotatedInCharacter().TryUseSkill(CpuParty.GetRotatedInCharacter());
                opponentTurn();
            }
        }

        int getDamageFromPlayerAttack()
        {
            int initialHealth = CpuParty.GetRotatedInCharacter().RemainingHealth;
            PlayerParty.GetRotatedInCharacter().AttackTarget(CpuParty.GetRotatedInCharacter());
            return initialHealth - CpuParty.GetRotatedInCharacter().RemainingHealth;
        }

        void opponentTurn()
        {
            Character attackedCpuCharacter = CpuParty.GetRotatedInCharacter();
            ensureBothPartiesCanContinue();
            if (CpuParty.GetRotatedInCharacter() == attackedCpuCharacter)
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
                game.RegisterDefeat(party.GetRotatedInCharacter());
                bool partyHasCharacters = party.TryRotateCharacter();
                if (!partyHasCharacters)
                {
                    bool playerWon = (party == CpuParty);
                    game.RegisterWinOrLoss(playerWon);
                }
            }
        }

        void letOpponentChooseMove()
        {
            CharacterAction decision = getCpuDecision();
            switch(decision)
            {
                case CharacterAction.Attack:
                    handleCpuAttack();
                    break;

                case CharacterAction.Skill:
                    CpuParty.GetRotatedInCharacter().TryUseSkill(PlayerParty.GetRotatedInCharacter());
                    game.RegisterUsedSkill(CpuParty.GetRotatedInCharacter(), PlayerParty.GetRotatedInCharacter());
                    break;
            }

        }

        void handleCpuAttack()
        {
            int initialHealth = PlayerParty.GetRotatedInCharacter().RemainingHealth;
            CpuParty.GetRotatedInCharacter().AttackTarget(PlayerParty.GetRotatedInCharacter());
            int damageDone = initialHealth - PlayerParty.GetRotatedInCharacter().RemainingHealth;
            game.RegisterAttack(CpuParty.GetRotatedInCharacter().Name, PlayerParty.GetRotatedInCharacter().Name, damageDone);
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
