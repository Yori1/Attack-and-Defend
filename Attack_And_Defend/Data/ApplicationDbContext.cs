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
            connection = new SqlConnection(Database.GetDbConnection().ConnectionString);
            connection.Close();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Hunter>();
            builder.Entity<Mage>();

            builder.Entity<Character>()
                .Ignore(c => c.Attack)
                .Ignore(c => c.AttacksPhysical)
                .Ignore(c => c.MagicDefense)
                .Ignore(c => c.MaximumHealth)
                .Ignore(c => c.PhysicalDefense)
                .Ignore(c => c.RemainingHealth)
                .Ignore(c => c.CanUseSkill)
                .Ignore(c => c.Fainted)
                .HasOne(c => c.Party);

            builder.Entity<Party>()
                .Ignore(p => p.IndexCharacterRotatedIn);

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Parties);

            builder.Entity<CombatResult>()
                .HasOne(a => a.User);

            var navigation = builder.Entity<Party>()
        .Metadata.FindNavigation(nameof(Party.Characters));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        #region EnsureCreationSqlObjects
        public void EnsureCreatedSqlObjects()
        {
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
            OpenClosedConnection();
            try
            {
                new SqlCommand(query, connection).ExecuteNonQuery();
            }

            catch (SqlException exc)
            {
                if (exc.Message != "There is already an object named '" + objectname + "' in the database.")
                    throw exc;
            }

            finally
            {
                connection.Close();
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
        #endregion

        #region UsedInPartyOverview
        public Dictionary<JobNumber, int> GetAmountForEveryJob()
        {
            OpenClosedConnection();
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
            connection.Close();
            return result;

        }

        public List<Party> GetPartiesUser(string username)
        {
            List<Party> parties = new List<Party>();
            OpenClosedConnection();
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
                    users.Add(ApplicationUsers.Where(u => u.Id == userId).First());
                    partyNames.Add(reader[2].ToString());
                    indexesLeadCharacter.Add(int.Parse(reader[3].ToString()));
                }
            }

            for (int x = 0; x < partyIds.Count(); x++)
            {
                Party party = new Party(users[x], partyNames[x], new List<Character>(), indexesLeadCharacter[x], partyIds[x]);
                AddCharacters(party);
                parties.Add(party);
            }

            connection.Close();
            return parties;
        }

        void AddCharacters(Party party)
        {
            OpenClosedConnection();
            var command = new SqlCommand("execute GetCharacters " + party.Id, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader[2].ToString();
                    int attack = int.Parse(reader[3].ToString());
                    int physicalDefense = int.Parse(reader[4].ToString());
                    int health = int.Parse(reader[5].ToString());
                    int magicDefense = int.Parse(reader[8].ToString());
                    int charactersDefeated = int.Parse(reader[9].ToString());
                    int experiencePoints = int.Parse(reader[10].ToString());
                    int matchesWon = int.Parse(reader[11].ToString());
                    int timesFainted = int.Parse(reader[12].ToString());
                    JobNumber jobNumber = (JobNumber)int.Parse(reader[7].ToString());

                    Character character = getImplementation(name, attack, magicDefense, physicalDefense, health, party, jobNumber);
                    party.TryAddCharacter(character);
                }
            }
            connection.Close();
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
            ApplicationUser user = ApplicationUsers.Where(u => u.UserName == username).First();
            user.Parties.Add(partyToAdd);
            if (user.Parties.Count() == 1)
                user.ChangeSelectedPartyIndex(0);
            return true;
        }

        public bool TryAddCharacter(string name, int attack, int magicDefense, int physicalDefense, int health, JobNumber jobNumber, int partyId)
        {
            Party partyToAddTo = Parties.Include(p=>p.Characters).Where(p => p.Id == partyId).FirstOrDefault();
            Character character = getImplementation(name, attack, magicDefense, physicalDefense, health, partyToAddTo, jobNumber);
            bool added = partyToAddTo.TryAddCharacter(character);
            if(added)
             Entry(partyToAddTo).State = EntityState.Modified;
            return added;
        }

        public void ChangeActiveParty(int partyIndex, string username)
        {
            ApplicationUser user = ApplicationUsers.Include(s=>s.Parties).Where(u => u.UserName == username).First();
            user.ChangeSelectedPartyIndex(partyIndex);
            Entry(user).State = EntityState.Modified;
        }

        string getUserIdFromPartyId(int partyId)
        {
            var queryUserId = from user in ApplicationUsers
                              join party in Parties on user.Id equals party.ApplicationUser.Id
                              where party.Id == partyId
                              select user.Id;
            string userId = queryUserId.First();
            return userId;
        }

        Character getImplementation(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, Party party, JobNumber jobNumber)
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

        #endregion

        #region UsedForCombat

        public Party GetActiveParty(string username)
        {
            ApplicationUser user = ApplicationUsers.Include(u=>u.Parties).Where(u => u.UserName == username).FirstOrDefault();
            int partyIdActiveParty = user.GetActiveParty().Id;
            var partiesFromUser = Parties.Include(p => p.Characters).Where(p => p.ApplicationUser.UserName == username);
            return partiesFromUser.Where(p => p.Id == partyIdActiveParty).FirstOrDefault();
        }

        public Party GetCpuParty(string cpuLevel)
        {
            return Parties.Include(p => p.Characters).Where(p => (p.Name == cpuLevel) && p.ApplicationUser == null).FirstOrDefault();
        }

        #endregion

        void OpenClosedConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void Complete()
        {
            SaveChanges();
        }
    }
}
