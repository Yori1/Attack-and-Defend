using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Logic;
using Attack_And_Defend.Models;
using Xunit;


namespace Attack_And_Defend_UnitTests
{
    public class JsonTests
    {
        [Fact]
        public void ParseParty()
        {
            Party party = new Party("s");
            party.TryAddCharacter(new Mage("d", 1, 1, 1, 1));

            JsonHandler handler = new JsonHandler();
            string jsonResult = handler.PartyToJson(party);
        }
    }
}
