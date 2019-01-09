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
        static public void Seed(EFContext context, UserManager<ApplicationUser> userManager)
        {
            EnsureCreationEnemyParties(context);
            EnsureCreationSampleUser(context, userManager);
            context.SaveChanges();
        }

        static void EnsureCreationEnemyParties(EFContext context)
        {
            if(!context.Parties.Any(p=>p.ApplicationUser == null))
            {
                Party party1 = new Party("1");
                for (int x = 0; x < 4; x++)
                    addLevel1Character(party1);

                context.Parties.Add(party1);
            }
        }

        static void addLevel1Character(Party partyToAddTo)
        {
            partyToAddTo.TryAddCharacter(Character.GetConcreteCharacter("Enemy", 2, 2, 2, 2, JobNumber.Hunter));
        }

        static void EnsureCreationSampleUser(EFContext context, UserManager<ApplicationUser> userManager)
        {
            string name = "SampleUser";
            if (!context.Users.Any(u => u.UserName == name))
            {
                ApplicationUser user = new ApplicationUser(name);
                var party = new Party("Party");

                for (int x = 0; x < 5; x++)
                    party.TryAddCharacter(Character.GetConcreteCharacter("testChar" + (x + 1), 2, 2, 2, 2, JobNumber.Mage));

                user.Parties.Add(party);

                var task = userManager.CreateAsync(user, "Password1!");
                task.Wait();
                context.SaveChanges();
            }
        }


        }


    }
