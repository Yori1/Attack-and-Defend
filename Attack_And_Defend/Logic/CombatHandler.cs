using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Models;
using Newtonsoft.Json;

namespace Attack_And_Defend.Logic
{
    public class CombatHandler
    {
        public string MessageLog { get { return Game.MessageLog; } }
        public bool? PlayerWon { get { return Game.Won; } }
        public int TurnNumber { get; private set; }
        public Game Game { get; private set; }

        public Party PlayerParty;
        public Party CpuParty;


        Random random;

        [JsonConstructor]
        public CombatHandler(Party playerParty, Party cpuParty, Game game, int turnNumber)
        {
            this.PlayerParty = playerParty;
            this.CpuParty = cpuParty;
            this.Game = game;
            TurnNumber = turnNumber;
        }

        public CombatHandler(Party playerParty, Party cpuParty, string username)
        {
            this.PlayerParty = playerParty;
            this.CpuParty = cpuParty;
            this.Game = new Game(username);
            TurnNumber = 0;
        }

        public void Attack()
        {
            int damage = getDamageFromPlayerAttack();
            Game.RegisterAttack(PlayerParty.GetRotatedInCharacter().Name, CpuParty.GetRotatedInCharacter().Name, damage);
            TurnNumber++;
            opponentTurn();
            TurnNumber++;
        }

        public void UseSkill()
        {
            PlayerParty.GetRotatedInCharacter().TryUseSkill(CpuParty.GetRotatedInCharacter());
            opponentTurn();
        }

        int getDamageFromPlayerAttack()
        {
            int initialHealth = CpuParty.GetRotatedInCharacter().RemainingHealth;
            PlayerParty.GetRotatedInCharacter().AttackTarget(CpuParty.GetRotatedInCharacter());
            return initialHealth - CpuParty.GetRotatedInCharacter().RemainingHealth;
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
                    Game.RegisterWinOrLoss(playerWon);
                }
            }
        }

        void letOpponentChooseMove()
        {
            CharacterAction decision = getCpuDecision();
            switch(decision)
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
