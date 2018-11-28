using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Attack_And_Defend.Models;
using Attack_And_Defend.Logic;
using Attack_And_Defend.Data;
using Microsoft.AspNetCore.Identity;

namespace Attack_And_Defend
{
    static public class DbInitializer
    {
        static public void Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            EnsureCreationEnemyParties(context);
            EnsureCreationSampleUser(context, userManager);
            context.EnsureCreatedSqlObjects();
            context.SaveChanges();
        }

        static void EnsureCreationEnemyParties(ApplicationDbContext context)
        {
            if(!context.Characters.Any(c=>c.Party.ApplicationUser == null))
            {
                Party party1 = new Party("1");
                for (int x = 0; x < 4; x++)
                    addLevel1Character(party1);

                context.Parties.Add(party1);
            }
        }

        static void addLevel1Character(Party partyToAddTo)
        {
            partyToAddTo.TryAddCharacter(getImplementation("Enemy", 2, 2, 2, 2, partyToAddTo, JobNumber.Hunter));
        }

        static void EnsureCreationSampleUser(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            string name = "SampleUser";
            if (!context.Users.Any(u => u.UserName == name))
            {
                ApplicationUser user = new ApplicationUser(name);
                var party = new Party("Party");

                for (int x = 0; x < 5; x++)
                    party.TryAddCharacter(getImplementation("testChar" + (x + 1), 2, 2, 2, 2, party, JobNumber.Mage));

                user.Parties.Add(party);

                var task = userManager.CreateAsync(user, "Password1!");
                task.Wait();
                context.SaveChanges();
            }
        }

        static Character getImplementation(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, Party party, JobNumber jobNumber)
        {
            Character character = null;
            switch(jobNumber)
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
