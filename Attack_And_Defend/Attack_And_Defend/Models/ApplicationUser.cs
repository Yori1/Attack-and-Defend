using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Attack_And_Defend.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ICollection<Party> Parties { get; private set; }

        public ApplicationUser()
        {
            Parties = new List<Party>();
        }

    }
}
