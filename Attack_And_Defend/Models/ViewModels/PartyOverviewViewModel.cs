using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Models;

namespace Models.ViewModels
{
    class PartyOverviewViewModel
    {
        public Character LeadCharacter { get; private set; }
        public List<Party> PartyList { get; private set; }

        public PartyOverviewViewModel(Character character, List<Party> parties)
        {
            LeadCharacter = character;
            PartyList = parties;
        }
    }
}
