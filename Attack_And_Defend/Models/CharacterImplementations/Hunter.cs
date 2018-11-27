using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Hunter: Character
    {
        public Hunter(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth) : base(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth)
        {
            this.JobNumber = JobNumber.Hunter;
            this.AttacksPhysical = true;
        }

       protected override void UseUniqueAction(Character target)
        {
            int damageToTakeTarget = target.MaximumHealth / 4;
            target.TakeDamage(damageToTakeTarget);
            Character characterAfterTarget = target.Party.GetNextCharacter();
            int damageToTakeAfterTarget = characterAfterTarget.MaximumHealth / 2;
            characterAfterTarget.TakeDamage(damageToTakeAfterTarget);
        }
    }
}
