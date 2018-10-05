using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;


using Attack_And_Defend.Models;
using Attack_And_Defend.Logic;

namespace RepositoryTest
{
    public class UnitTest1
    {
        PartyRepository partyRepository = new PartyRepository();

        [Fact]
        public void CheckSeededParty()
        {
            List<Party> parties = partyRepository.GetPartiesUser("SampleUser");
            Party sampleParty = parties.First();

            Assert.True(sampleParty.Name == "Party");
            Assert.True(sampleParty.Characters.First().Name == "testChar1");
            Assert.True(sampleParty.Characters.Last().Name == "testChar5");
        }

        [Fact]
        public void AddParty()
        {
            partyRepository.TryAddParty("PartyAddedInUnitTest", "SampleUser");
            var partyAdded = partyRepository.GetPartiesUser("SampleUser").Last();
            Assert.True(partyAdded.Name == "PartyAddedInUnitTest");
        }
 
    }
}
