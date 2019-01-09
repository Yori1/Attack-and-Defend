using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Attack_And_Defend.Models
{
    public class Mage : Character
    {
        public Mage(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, int experiencePoints = 0,
            int timesFainted = 0, int charactersDefeated = 0, int matchesWon = 0)
            : base(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth, experiencePoints, timesFainted, charactersDefeated, matchesWon)
        {
            this.JobNumber = JobNumber.Mage;
            this.AttacksPhysical = false;
        }

        protected override void UseUniqueAction(Character target)
        {
            int healthToBringDownTo = (int)(target.MaximumHealth*0.1);
            if(target.RemainingHealth > healthToBringDownTo)
            {
                int damageToTake = target.RemainingHealth - healthToBringDownTo;
                target.TakeDamage(damageToTake);
            }
        }
    }
}
