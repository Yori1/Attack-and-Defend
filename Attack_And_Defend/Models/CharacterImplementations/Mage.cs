using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Mage : Character
    {
        public Mage(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth)
            : base(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth)
        {
            this.JobNumber = JobNumber.Mage;
            this.AttacksPhysical = false;
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
