using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Attack_And_Defend.Models;

namespace Attack_And_Defend.Data
{
    public class PartyApplicationContext : IPartyContext
    {
        EFContext context;
        SqlConnection connection;

        public PartyApplicationContext(EFContext context)
        {
            this.context = context;
            connection = new SqlConnection(context.Database.GetDbConnection().ConnectionString);
        }

        public Dictionary<JobNumber, int> GetAmountForEveryJob()
        {
            connection.Open();
            var command = new SqlCommand("execute GetTotalNumberOfCharactersEveryJob", connection);
            var result = new Dictionary<JobNumber, int>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    JobNumber job = (JobNumber)int.Parse(reader[0].ToString());
                    int charactersWithJob = int.Parse(reader[1].ToString());
                    result.Add(job, charactersWithJob);
                }
            }
            return result;
        }

        public List<Party> GetPartiesUser(string username)
        {
            List<Party> parties = new List<Party>();
            connection.Open();
            List<int> partyIds = new List<int>();
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<string> partyNames = new List<string>();
            List<int> indexesLeadCharacter = new List<int>();

            var command = new SqlCommand("execute GetParties " + username, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    partyIds.Add(int.Parse(reader[0].ToString()));
                    string userId = reader[1].ToString();
                    users.Add(context.ApplicationUsers.Where(u => u.Id == userId).First());
                    partyNames.Add(reader[2].ToString());
                    indexesLeadCharacter.Add(int.Parse(reader[3].ToString()));
                }
            }

            for (int x = 0; x < partyIds.Count(); x++)
            {
                List<Character> charactersInParty = getCharacters(partyIds[x]);
                Party party = new Party(users[x], partyNames[x], charactersInParty, indexesLeadCharacter[x], partyIds[x]);
                parties.Add(party);
            }

            return parties;
        }

        List<Character> getCharacters(int partyId)
        {
            return context.Parties.Where(p => p.Id == partyId).Include(p => p.Characters).FirstOrDefault()?.Characters?.ToList();
        }

        public bool TryAddParty(string name, string username)
        {
            string nameToUse = name;
            if (name == "")
                return false;
            var query = from party in context.Parties where party.Name == nameToUse && party.ApplicationUser.UserName == username select party;
            if (query.Count() > 0)
                return false;
            Party partyToAdd = new Party(name);
            context.ApplicationUsers.Where(u => u.UserName == username).First().Parties.Add(partyToAdd);
            return true;
        }

        public bool TryAddCharacter(Character character, int idPartyToAddTo)
        {
            var partyToAddToQuery = from party in context.Parties where party.Id == idPartyToAddTo select party;
            Party partyToAddTo = partyToAddToQuery.First();


            return partyToAddTo.TryAddCharacter(character);
        }

        public void ChangeActiveParty(int partyIndex, string username)
        {
            ApplicationUser user = context.ApplicationUsers.Include(s => s.Parties).Where(u => u.UserName == username).First();
            user.ChangeSelectedPartyIndex(partyIndex);
            context.Entry(user).State = EntityState.Modified;
        }

        string getUserIdFromPartyId(int partyId)
        {
            var queryUserId = from user in context.ApplicationUsers
                              join party in context.Parties on user.Id equals party.ApplicationUser.Id
                              where party.Id == partyId
                              select user.Id;
            string userId = queryUserId.First();
            return userId;
        }

        public void ChangeIndexParty(int indexToChange, int partyId)
        {
            var party = context.Parties.Where(p => p.Id == partyId).Include(p=>p.Characters).First();
            party.ChangeLeadCharacter(indexToChange);
            context.Entry(party).State = EntityState.Modified;
        }

        public void ChangeActiveParty(string username, int index)
        {
            ApplicationUser user = context.ApplicationUsers.Include(a=>a.Parties).Where(a => a.UserName == username).First();
            user.ChangeSelectedPartyIndex(index);
        }

        public void Complete()
        {
            context.SaveChanges();
        }
    }
}
