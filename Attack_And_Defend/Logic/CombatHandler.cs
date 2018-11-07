using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Models;

namespace Attack_And_Defend.Logic
{
    public class CombatHandler
    {
        public Party PlayerParty;
        public Party CpuParty;

        List<Party> parties;

        public bool? PlayerWon = null;

        public CombatHandler(Party playerParty, Party cpuParty)
        {
            this.PlayerParty = playerParty;
            this.CpuParty = cpuParty;
        }

        public void Attack()
        {
            PlayerParty.ActiveCharacter.AttackTarget(CpuParty.ActiveCharacter);
            ensureBothPartiesCanContinue();
            if (PlayerWon == null)
            {
                letOpponentChooseMove();
                ensureBothPartiesCanContinue();
            }
        }

        void letOpponentChooseMove()
        {
            CpuParty.ActiveCharacter.AttackTarget(PlayerParty.ActiveCharacter);
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
                bool partyOutOfCharacters = party.TryRotateActiveCharacter();
                if (partyOutOfCharacters)
                    PlayerWon = (party == PlayerParty);
            }
        }

        CPUDecision makeRandomDecision(Character cpuCharacter)
        {
            Random random = new Random();
            if (cpuCharacter.CanUseSkill)
            {
                if (random.Next(0, 1) == 1)
                    return CPUDecision.Skill;
            }
            return CPUDecision.Attack;
        }

        enum CPUDecision
        {
            Attack,
            Skill
        }

    }

}
