using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class CharacterDetailsViewModel
    {
        public int PartyId { get; set; }

        public CharacterDetailsViewModel(int partyId)
        {
            PartyId = partyId;
        }
    }
}
