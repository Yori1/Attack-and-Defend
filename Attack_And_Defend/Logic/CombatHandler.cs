using System;
using System.Collections.Generic;
using System.Text;

using Attack_And_Defend.Models;
using Attack_And_Defend.Data;

namespace Logic
{
    class CombatHandler
    {
        Party playerParty;
        Party cpuParty;

        Character playerCharacter;
        Character cpuCharacter;

        public CombatHandler(Party playerParty, Party cpuParty)
        {
            this.playerParty = playerParty;
            this.cpuParty = cpuParty;
        }

        public void Attack()
        {
        }
    }
}
