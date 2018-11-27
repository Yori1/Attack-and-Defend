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
        Character playeractive;
        Character cpuActive;

        public CombatHandlerTests()
        {
            combatHandler = new CombatHandler(makeTestParty("playerParty"), makeTestParty("cpuParty"), "username");

            cpuparty = combatHandler.CpuParty;
            playerparty = combatHandler.PlayerParty;
            playeractive = combatHandler.PlayerParty.ActiveCharacter;
            cpuActive = combatHandler.CpuParty.ActiveCharacter;
        }

        [Fact]
        public void InitializingParties()
        {
            while (combatHandler.PlayerWon == null)
                combatHandler.Attack();
        }

        Party makeTestParty(string name)
        {
            Party party = new Party(name);
            fillCharacterList(party);
            return party;
        }

        void fillCharacterList(Party party)
        {
            for (int x = 0; x < 5; x++)
            {
                if ((double)x / 2 == x / 2)
                    party.TryAddCharacter(new Mage("Character#" + x, 2, 2, 2, 2));
                else
                    party.TryAddCharacter(new Hunter("Character#" + x, 2, 2, 2, 2));
            }
        }


    }
}
