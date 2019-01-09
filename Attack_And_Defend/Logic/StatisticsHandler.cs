using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Attack_And_Defend.Models;
using Attack_And_Defend.Data;

namespace Attack_And_Defend.Logic
{
    public class StatisticsHandler
    {
        EFContext context;
        public StatisticsHandler(EFContext context)
        {
            this.context = context;
        }

        public Dictionary<JobNumber, int> GetAmountForEveryJob()
        {
            Dictionary<JobNumber, int> keyValuePairs = new Dictionary<JobNumber, int>();
            var query = from Character character in context.Characters
                        group character by character.JobNumber into g
                        select new KeyValuePair<JobNumber, int>(g.Key, g.Count());

            foreach (var pair in query)
                keyValuePairs.Add(pair.Key, pair.Value);

            return keyValuePairs;
        }

    }
}
