using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Attack_And_Defend.Models
{
    public class Hunter: Character
    {
        public Hunter(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, int experiencePoints = 0,
            int timesFainted = 0, int charactersDefeated = 0, int matchesWon = 0)
            : base(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth, experiencePoints, timesFainted, charactersDefeated, matchesWon)
        {
            this.JobNumber = JobNumber.Hunter;
            this.AttacksPhysical = true;
        }

        protected override void UseUniqueAction(Character target)
        {
            int damageToTakeTarget = target.MaximumHealth / 4;
            target.TakeDamage(damageToTakeTarget);
            int damageToTakeAfterTarget = target.NextCharacterInParty.MaximumHealth / 2;
            target.NextCharacterInParty.TakeDamage(damageToTakeAfterTarget);
        }
    }
}
