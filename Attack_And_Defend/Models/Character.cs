using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Attack_And_Defend.Models
{
    public abstract class Character
    {
        public int Id { get; private set; }

        [JsonIgnore]
        public Party Party { get; private set; }
        public string Name { get; private set; }
        public int Attack { get; private set; }
        public int MagicDefense { get; private set; }
        public int PhysicalDefense { get; private set; }
        public int MaximumHealth { get; private set; }
        public int RemainingHealth { get; private set; }
        public bool CanUseSkill { get; private set; } = true;
        public bool AttacksPhysical { get; private set; }
        public JobNumber JobNumber { get; protected set; }

        public bool Fainted;

        public Character(string name, int attack, int magicDefense, int physicalDefense, int maximumHealth)
        {
            Name = name;
            Attack = attack;
            MagicDefense = magicDefense;
            PhysicalDefense = physicalDefense;
            MaximumHealth = maximumHealth;
        }

        public abstract void UseUniqueAction(Character target);

        public void AttackTarget(Character target)
        {
            target.TakeDamage(Attack, AttacksPhysical);
        }

        public void TakeDamage(int attackStatAttacker, bool attackerAttacksPhysical)
        {
            int damageToTake;

            if (attackerAttacksPhysical)
                damageToTake = attackStatAttacker - PhysicalDefense;
            else
                damageToTake = attackStatAttacker - MagicDefense;

            MaximumHealth -= damageToTake;

            if (MaximumHealth < 1)
            {
                Fainted = true;
            }
        }

        public static Character GetConcreteCharacter(string name, int attack, int magicDefense, int physicalDefense, int health, JobNumber jobNumber)
        {
            switch (jobNumber)
            {
                case JobNumber.Hunter:
                    return new Hunter(name, attack, magicDefense, physicalDefense, health);

                case JobNumber.Mage:
                    return new Mage(name, attack, magicDefense, physicalDefense, health);

                default:
                    return null;
            }
        }
    }
}
