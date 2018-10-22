using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    class CombatResults
    {
        public int UserId { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public DateTime LastTimePlayed { get; set; }
    }
}
