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
    public class EFContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; private set; }
        public DbSet<Party> Parties { get; private set; }
        public DbSet<Character> Characters { get; private set; }

        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Character>()
                .Ignore(c => c.Attack)
                .Ignore(c => c.AttacksPhysical)
                .Ignore(c => c.MagicDefense)
                .Ignore(c => c.MaximumHealth)
                .Ignore(c => c.PhysicalDefense)
                .Ignore(c => c.RemainingHealth)
                .Ignore(c => c.CanUseSkill)
                .Ignore(c => c.Fainted)
                .HasOne(c => c.NextCharacterInParty);



            builder.Entity<Hunter>();

            builder.Entity<Mage>();

            builder.Entity<Party>()
                .HasMany(a => a.Characters);

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Parties);

            builder.Entity<CombatResult>()
                .HasOne(a => a.User);

            base.OnModelCreating(builder);
        }

    }
}