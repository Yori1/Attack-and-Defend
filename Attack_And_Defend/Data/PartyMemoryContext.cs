using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attack_And_Defend.Models;


namespace Attack_And_Defend.Data
{
    public class PartyMemoryContext : IPartyContext
    {
        List<TestableUser> applicationUsers = new List<TestableUser>();
        List<Character> characters = new List<Character>();

        public PartyMemoryContext()
        {
            makeSampleUser();
        }

        void makeSampleUser()
        {
            string name = "SampleUser";
            applicationUsers.Add(new TestableUser(name, 0));

            var query = from sampleuser in applicationUsers where sampleuser.UserName == "SampleUser" select sampleuser;
            TestableUser sampleUser = query.First();
            var party = new Party("Party");

            for (int x = 0; x < 5; x++)
                party.TryAddCharacter(Character.GetConcreteCharacter("testChar" + (x + 1), 2, 2, 2, 2, JobNumber.Mage));

            sampleUser.Parties.Add(party);
        }

        void addUser(string username)
        {
            int idNewUser = applicationUsers.OrderBy(u => u.Id).First().Id + 1;
            var user = new TestableUser(username, idNewUser);

        }

        public List<Party> GetPartiesUser(string username)
        {
            var query = from user in applicationUsers where user.UserName == username select user;
            return query.First().Parties;
        }

        List<Character> getCharacters(int partyID)
        {
            var query = from Party party in getAllParties() where party.Id == partyID select party;
            return query.FirstOrDefault().Characters.ToList();
        }

        public bool TryAddCharacter(Character character, int idPartyToAddTo)
        {

            if (getCharacters(idPartyToAddTo).Count() >= 5)
                return false;

            var partyToAddToQuery = from party in getAllParties() where party.Id == idPartyToAddTo select party;
            Party partyToAddTo = partyToAddToQuery.First();
            partyToAddTo.TryAddCharacter(character);
            return true;
        }
        public bool TryAddParty(string name, string username)
        {
            string nameToUse = name;
            if (name == "")
                return false;
            var query = from party in getAllParties() where party.Name == nameToUse select party;
            if (query.Count() > 0)
                return false;
            Party partyToAdd = new Party(null, name, null);
            applicationUsers.Where(u => u.UserName == username).First().Parties.Add(partyToAdd);
            return true;
        }

        List<Party> getAllParties()
        {
            List<Party> parties = new List<Party>();
            foreach (TestableUser user in applicationUsers)
                foreach (Party party in user.Parties)
                    parties.Add(party);

            return parties;

        }

        public void Complete()
        {
            return;
        }

        public Dictionary<JobNumber, int> GetAmountForEveryJob()
        {
            var query = from character in characters
                        group character by character.JobNumber into jobs
                        select new { job = jobs.First().JobNumber, count = jobs.Count() };
            Dictionary<JobNumber, int> jobAndAmount = new Dictionary<JobNumber, int>();
            foreach(var pair in query)
            {
                jobAndAmount.Add(pair.job, pair.count);
            }
            return jobAndAmount;
        }

        public void ChangeIndexParty(int indexToChange, int partyId)
        {
            var party = getAllParties().Where(p => p.Id == partyId).First();
            if (party != null)
                party.ChangeLeadCharacter(indexToChange);
        }

        public void ChangeActiveParty(string username, int index)
        {
            throw new NotImplementedException();
        }
    }
}
