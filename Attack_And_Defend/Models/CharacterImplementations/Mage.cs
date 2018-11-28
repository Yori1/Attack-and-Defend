using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Mage : Character
    {
        public Mage(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, Party party, int experiencePoints = 0,
            int timesFainted = 0, int charactersDefeated = 0, int matchesWon = 0)
            : base(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth, party, experiencePoints, timesFainted, charactersDefeated, matchesWon)
        {
            this.JobNumber = JobNumber.Hunter;
            this.AttacksPhysical = true;
        }

        public Mage(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth,
   int experiencePoints, int timesFainted, int charactersDefeated, int matchesWon) : base(name, baseAttack, baseMagicDefense, basePhysicalDefense,
       baseMaximumHealth, experiencePoints, timesFainted, charactersDefeated, matchesWon)
        {
            this.JobNumber = JobNumber.Hunter;
            this.AttacksPhysical = true;
        }

        protected override void UseUniqueAction(Character target)
        {
            int healthToBringDownTo = (int)(target.MaximumHealth*0.1);
            if(target.RemainingHealth > healthToBringDownTo)
            {
                int damageToTake = target.MaximumHealth - healthToBringDownTo;
                target.TakeDamage(damageToTake);
            }
        }
    }
}
