using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Hunter: Character
    {
       public Hunter(string name, int attack, int magicDefense, int physicalDefense, int health) : base(name, attack, magicDefense, physicalDefense, health)
        { this.JobNumber = JobNumber.Hunter; }

        public override void UseUniqueAction(Character target)
        {
            throw new NotImplementedException();
        }
    }
}
