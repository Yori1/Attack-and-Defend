﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Attack_And_Defend.Models
{
    [Table("AspNetUsers")]
    public class ApplicationUser: IdentityUser
    {
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Party> Parties { get; private set; } = new List<Party>();
        public int SelectedPartyIndex { get; private set; }

        public ApplicationUser() { }

        public ApplicationUser(string username) : base(username)
        {
            Parties = new List<Party>();
        }

        public void ChangeSelectedPartyIndex(int indexToChangeTo)
        {
            if (indexToChangeTo > Parties.Count() - 1)
            { throw new IndexOutOfRangeException(); }

            SelectedPartyIndex = indexToChangeTo;
        }

        public void AddParty(Party party)
        {
            if (party.Characters.Count() == 0)
                SelectedPartyIndex = 0;
        }


        public Party GetActiveParty()
        {
            return Parties.ElementAt(SelectedPartyIndex);
        }
    }
}
