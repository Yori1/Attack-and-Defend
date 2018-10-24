using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Attack_And_Defend.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ICollection<Party> Parties { get; private set; } = new List<Party>();
        public int SelectedPartyIndex { get; private set; }

        public ApplicationUser() { }

        public ApplicationUser(string username) : base(username)
        {
            Parties = new List<Party>();
        }

    }
}
