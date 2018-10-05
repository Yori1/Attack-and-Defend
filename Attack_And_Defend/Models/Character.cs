using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class Character
    {
        public int Id { get; private set; }

        public Party Party { get; private set; }
        public string Name { get; private set; }
        public int Attack { get; private set; }
        public int MagicDefense { get; private set; }
        public int PhysicalDefense { get; set; }
        public int Health { get; private set; }
        public bool Fainted { get; private set; }


        public JobNumber JobNumber { get; set; }

        Job job;
        public bool AttacksPhysical { get; }

        public Character()
        {
           
        }

        public Character(string name, int attack, int magicDefense, int physicalDefense, int health, JobNumber jobNumber)
        {
            Name = name;
            Attack = attack;
            MagicDefense = magicDefense;
            PhysicalDefense = physicalDefense;
            Health = health;
            JobNumber = jobNumber;

            job = jobNumberToJobImplementation(jobNumber);
        }

        public string GetJobName()
        {
            return JobNumber.ToString();
        }

        public void AttackTarget(Character target)
        {

        }

        public void TakeDamage(int damageToTake)
        {

        }

        public void UseUniqueAction(Character target)
        { }

        Job jobNumberToJobImplementation(JobNumber jobNumber)
        {
            switch(jobNumber)
            {
                case JobNumber.Hunter:
                    return new Hunter();

                case JobNumber.Mage:
                    return new Mage();

                default:
                    return null;
            }
        }
    }
}
