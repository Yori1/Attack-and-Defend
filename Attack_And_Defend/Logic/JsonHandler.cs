using System;
using System.Collections.Generic;
using System.Text;
using Attack_And_Defend.Logic;
using Attack_And_Defend.Models;
using Newtonsoft.Json;
using System.IO;

namespace Attack_And_Defend.Logic
{
    public class JsonHandler
    {
        public JsonHandler()
        {

        }

        public string PartyToJson(Party party)
        {
            return JsonConvert.SerializeObject(party);
        }

        public Party JsonToParty(string json)
        {
            return JsonConvert.DeserializeObject<Party>(json);
        }
    }
}
