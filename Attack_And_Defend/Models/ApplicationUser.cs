using System;
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
        public ICollection<Party> Parties { get; private set; } = new List<Party>();
        public int SelectedPartyIndex { get; private set; }

        public ApplicationUser() { }

        public ApplicationUser(string username) : base(username)
        {
            Parties = new List<Party>();
        }

    }
}
