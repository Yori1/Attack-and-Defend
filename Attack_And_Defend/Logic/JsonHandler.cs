using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Attack_And_Defend.Models;

namespace Attack_And_Defend.Logic
{
    public static class JsonHandler
    {
        public static string ToJson(CombatHandler combatHandler)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                serializer.Serialize(writer, combatHandler, typeof(CombatHandler));
            }

            string json = sb.ToString();
            return json;
        }

        public static CombatHandler FromJson(string json)
        {
            CombatHandler obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CombatHandler>(json, new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });
            fixLoopReferencesCombatHandler(obj);
            return obj;
        }

        static void fixLoopReferencesCombatHandler(CombatHandler combatHandler)
        {
            fixLoopReferencesParty(combatHandler.CpuParty);
            fixLoopReferencesParty(combatHandler.PlayerParty);
        }

        static void fixLoopReferencesParty(Party party)
        {
            foreach(Character character in party.Characters)
            {
                character.Party = party;
            }
        }
    }
}
