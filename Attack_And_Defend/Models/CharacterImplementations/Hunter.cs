using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Hunter: Character
    {
       public Hunter(string name, int attack, int magicDefense, int physicalDefense, int maximumHealth) : base(name, attack, magicDefense, physicalDefense, maximumHealth)
        {
            this.JobNumber = JobNumber.Hunter;
            this.AttacksPhysical = true;
        }

        public override void UseUniqueAction(Character target)
        {
            throw new NotImplementedException();
        }
    }
}
