using System;
using System.Collections.Generic;
using System.Text;

namespace Attack_And_Defend.Models
{
    public class CombatResult
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public DateTime LastTimePlayed { get; set; }
    }
}
