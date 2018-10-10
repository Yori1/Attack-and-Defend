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
    public class ApplicationDbContext : IdentityDbContext, ITestableContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; private set; }
        public DbSet<Party> Parties { get; private set; }
        public DbSet<Character> Characters { get; private set; }

        SqlConnection connection;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            EnsureCreatedSqlObjects();
        }

        void EnsureCreatedSqlObjects()
        {
            connection = new SqlConnection(Database.GetDbConnection().ConnectionString);
            connection.Open();
            EnsureCreatedRegisteredTrigger();
            EnsureCreatedProcedureAmountOfJobs();
            connection.Close();
        }

        void EnsureObjectCreated(string query, string objectname)
        {
            try
            {
                new SqlCommand(query, connection).ExecuteNonQuery();
            }

            catch (SqlException exc)
            {
                if (exc.Message != "There is already an object named '"+ objectname +"' in the database.")
                    throw exc;
            }
        }

        void EnsureCreatedProcedureAmountOfJobs()
        {
            EnsureObjectCreated("create procedure GetTotalNumberOfCharactersEveryJob as select JobNumber, count(*) as " +
                "TotalCharactersWithJob from Characters group by JobNumber", "GetTotalNumberOfCharactersEveryJob");
        }

        void EnsureCreatedRegisteredTrigger()
        {
            EnsureObjectCreated("CREATE TRIGGER LogUserRegistered on AspNetUsers after insert as begin insert into UserLog" +
               "(LogDateTime, LogDescription, UserId, Username) select getdate(), 'User has been inserted.', i.Id, i.UserName" +
               " from AspNetUsers u inner join inserted i on u.Id = i.Id end", "LogUserRegistered");
        }

        public Dictionary<JobNumber, int> GetAmountForEveryJob()
        {
            connection.Open();
            var command = new SqlCommand("execute GetTotalNumberOfCharactersEveryJob", connection);
            var result = new Dictionary<JobNumber, int>();
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
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
            var query = from party in Parties where party.ApplicationUser.UserName == username select party;
            var partylist = query.ToList();

            foreach (Party party in partylist)
            {
                getCharacters(party.Id);
            }

            return partylist;
        }

        void getCharacters(int partyID)
        {
             var query = from character in Characters where character.Party.Id == partyID select character;
            var list = query.ToList();
        }

        public bool TryAddParty(string name, string username)
        {
            string nameToUse = name;
            if (name == "")
                return false;
            var query = from party in Parties where party.Name == nameToUse select party;
            if (query.Count() > 0)
                return false;
            Party partyToAdd = new Party(name);
            ApplicationUsers.Where(u => u.UserName == username).First().Parties.Add(partyToAdd);
            return true;
        }

        public bool TryAddCharacter(Character character, int idPartyToAddTo)
        {
            var partyToAddToQuery = from party in Parties where party.Id == idPartyToAddTo select party;
            Party partyToAddTo = partyToAddToQuery.First();
            return partyToAddTo.TryAddCharacter(character);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Parties);
        }

        public void Complete()
        {
            SaveChanges();
        }
    }
}
