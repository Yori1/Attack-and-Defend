using System;
using System.Collections.Generic;
using System.Linq;
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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
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
