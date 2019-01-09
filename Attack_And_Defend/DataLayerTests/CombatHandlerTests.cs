using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Logic;
using Attack_And_Defend.Models;
using Xunit;

namespace Attack_And_Defend_UnitTests
{
    public class CombatHandlerTests
    {
        CombatHandler combatHandler;
        Party cpuparty;
        Party playerparty;

        public CombatHandlerTests()
        {
            combatHandler = new CombatHandler(makeTestParty("playerParty"), makeTestParty("cpuParty"),0 , "username");

            cpuparty = combatHandler.CpuParty;
            playerparty = combatHandler.PlayerParty;
        }

        [Fact]
        public void RotateCharacterWhenActiveCharacterDefeated()
        {
            Character leadCpuCharacter = cpuparty.GetRotatedInCharacter();
            Character switchedInPlayerCharacterBeforeDefeat = playerparty.GetRotatedInCharacter();
            while(switchedInPlayerCharacterBeforeDefeat.RemainingHealth>0)
            {
                combatHandler.Attack();
            }
            Character switchedInPlayerCharacterAfterDefeat = playerparty.GetRotatedInCharacter();

            Assert.True(switchedInPlayerCharacterBeforeDefeat.Fainted == true);
            Assert.True(switchedInPlayerCharacterAfterDefeat.Name != switchedInPlayerCharacterBeforeDefeat.Name);
        }

        [Fact]
        public void UsingMageSkill()
        {
            RotateCharacterWhenActiveCharacterDefeated();
            Character switchedInCpuCharacter = cpuparty.GetRotatedInCharacter();
            Character switchedInPlayerCharacter = playerparty.GetRotatedInCharacter();

            Assert.True(switchedInPlayerCharacter is Mage);
            combatHandler.UseSkill();
            int targetExpectedHealth = (int)(switchedInCpuCharacter.MaximumHealth * 0.1);
            Assert.True(switchedInCpuCharacter.RemainingHealth == targetExpectedHealth);
        }

        Party makeTestParty(string name)
        {
            Party party = new Party(name);
            fillCharacterList(party);
            return party;
        }

        void fillCharacterList(Party party)
        {
            for (int x = 1; x < 6; x++)
            {
                if ((double)x / 2 == x / 2)
                    party.TryAddCharacter(new Mage("Character#" + x, 2, 2, 2, 2));
                else
                    party.TryAddCharacter(new Hunter("Character#" + x, 2, 2, 2, 2));
            }

            party.SetNextCharacters();
        }


    }
}
