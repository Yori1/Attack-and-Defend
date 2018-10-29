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
        public DbSet<CombatResult> CombatResults { get; private set; }

        SqlConnection connection;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            EnsureCreatedSqlObjects();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Hunter>();

            builder.Entity<Mage>();

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Parties);

            builder.Entity<CombatResult>()
                .HasOne(a => a.User);
        }

        void EnsureCreatedSqlObjects()
        {
            connection = new SqlConnection(Database.GetDbConnection().ConnectionString);
            connection.Open();
            EnsureCreatedRegisteredTrigger();
            EnsureCreatedProcedureAmountOfJobs();
            EnsureCreatedProcedureGetParties();
            EnsureCreatedProcedureGetCharacters();
            EnsureCreatedUserLogTable();
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

        void EnsureCreatedProcedureGetParties()
        {
            EnsureObjectCreated("create procedure GetParties " +
                "@Username nvarchar(200) " +
                "as begin " +
                "select * from dbo.Parties p where (select subU.UserName from dbo.AspNetUsers subU where subU.Id = p.ApplicationUserId) = @Username; " +
                "end", "GetParties");
        }

        void EnsureCreatedProcedureGetCharacters()
        {
            EnsureObjectCreated(
                "create procedure GetCharacters @PartyId int " +
                "as " +
                "begin " +
                "select * from Characters ch where ch.PartyId = @PartyId " +
                "end", "GetCharacters");

        }

        void EnsureCreatedRegisteredTrigger()
        {
            EnsureObjectCreated("CREATE TRIGGER LogUserRegistered on AspNetUsers after insert as begin insert into UserLog" +
               "(LogDateTime, LogDescription, UserId, Username) select getdate(), 'User has been inserted.', i.Id, i.UserName" +
               " from AspNetUsers u inner join inserted i on u.Id = i.Id end", "LogUserRegistered");
        }

        void EnsureCreatedUserLogTable()
        {
            EnsureObjectCreated("CREATE TABLE [dbo].[UserLog] ( [Id]  INT  IDENTITY(1, 1) NOT NULL, " +
                "[LogDateTime]    DATETIME      NULL, " +
                "[LogDescription] NVARCHAR(50) NULL, " +
                "[UserId]         NVARCHAR(50) NULL, " +
                "[Username]       NVARCHAR(50) NULL, " +
                "PRIMARY KEY CLUSTERED([Id] ASC)" +
                ");", "UserLog");
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
            List<Party> parties = new List<Party>();
            connection.Open();
            List<int> partyIds = new List<int>();
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<string> partyNames = new List<string>();

            var command = new SqlCommand("execute GetParties " + username, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    partyIds.Add(int.Parse(reader[0].ToString()));
                    string userId = reader[1].ToString();
                    users.Add(ApplicationUsers.Where(u => u.Id == userId).First());
                    partyNames.Add(reader[2].ToString());
                }
            }

            for(int x=0; x<partyIds.Count(); x++)
            {
                List<Character> charactersInParty = getCharacters(partyIds[x]);
                Party party = new Party(users[x], partyNames[x], charactersInParty, partyIds[x]);
                parties.Add(party);
            }

            return parties;
        }

        List<Character> getCharacters(int partyId)
        {
            List<Character> result = new List<Character>();
            var command = new SqlCommand("execute GetCharacters " + partyId, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader[2].ToString();
                    int attack = int.Parse(reader[3].ToString());
                    int magicDefense = int.Parse(reader[4].ToString());
                    int health = int.Parse(reader[5].ToString());
                    int physicalDefense = int.Parse(reader[6].ToString());
                    JobNumber jobNumber = (JobNumber)int.Parse(reader[10].ToString());

                    Character character = Character.GetConcreteCharacter(name,attack,magicDefense,health,physicalDefense, jobNumber);
                    result.Add(character);
                }
            }
            return result;
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


        public void Complete()
        {
            SaveChanges();
        }
    }
}
