using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Models;

namespace Attack_And_Defend.Models
{
    public class PartyOverviewViewModel
    {
        public int IndexSelectedParty { get; private set; }
        public List<Party> PartyList { get; private set; }

        public PartyOverviewViewModel(int indexSelectedParty, List<Party> partyList)
        {
            IndexSelectedParty = indexSelectedParty;
            PartyList = partyList;
        }
    }
}
