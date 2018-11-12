using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Mage : Character
    {
        public Mage(string name, int attack, int magicDefense, int physicalDefense, int maximumHealth) : base(name, attack, magicDefense, physicalDefense, maximumHealth)
        {
            this.JobNumber = JobNumber.Mage;
            this.AttacksPhysical = false;
        }

        public override void UseUniqueAction(Character target)
        {
            throw new NotImplementedException();
        }
    }
}
