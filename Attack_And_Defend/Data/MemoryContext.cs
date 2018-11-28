using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attack_And_Defend.Models;


namespace Attack_And_Defend.Data
{
    public class MemoryContext : ITestableContext
    {
        List<TestableUser> applicationUsers = new List<TestableUser>();
        List<Character> characters = new List<Character>();

        public MemoryContext()
        {
            makeSampleUser();
        }

        void makeSampleUser()
        {
            string name = "SampleUser";
            applicationUsers.Add(new TestableUser(name, 0));

            var query = from sampleuser in applicationUsers where sampleuser.UserName == "SampleUser" select sampleuser;
            TestableUser sampleUser = query.First();
            var party = new Party(null, "Party", null);

            for (int x = 0; x < 5; x++)
                party.TryAddCharacter(new Mage("testChar" + (x + 1), 2, 2, 2, 2, party));

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

        void getCharacters(int partyID)
        {
            var query = from character in characters where character.Party.Id == partyID select character;
            var list = query.ToList();
        }

        public bool TryAddCharacter(string name, int attack, int magicDefense, int physicalDefense, int health, JobNumber jobNumber, int partyId)
        {
            List<Party> parties = getAllParties();
            Party party = parties.Where(p => p.Id == partyId).FirstOrDefault();
            party.TryAddCharacter(getImplementation(name, attack, magicDefense, physicalDefense, health, party, jobNumber));
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
                        group character by character.GetType() into jobs
                        select new { job = jobs.First().JobNumber, count = jobs.Count() };
            Dictionary<JobNumber, int> jobAndAmount = new Dictionary<JobNumber, int>();
            foreach (var pair in query)
            {
                jobAndAmount.Add(pair.job, pair.count);
            }
            return jobAndAmount;
        }

        static Character getImplementation(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, Party party, JobNumber jobNumber)
        {
            Character character = null;
            switch (jobNumber)
            {
                case JobNumber.Hunter:
                    character = new Hunter(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth, party);
                    break;

                case JobNumber.Mage:
                    character = new Hunter(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth, party);
                    break;
            }

            return character;
        }
    }
}
