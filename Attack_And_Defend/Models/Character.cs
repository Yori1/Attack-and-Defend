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

        public Party Party { get; private set; }
        public string Name { get; private set; }

        public int Attack { get; private set; }
        public int MagicDefense { get; private set; }
        public int PhysicalDefense { get; private set; }
        public int MaximumHealth { get; private set; }

        public int BaseAttack { get; private set; }
        public int BaseMagicDefense { get; private set; }
        public int BasePhysicalDefense { get; private set; }
        public int BaseMaximumHealth { get; private set; }

        public bool AttacksPhysical { get; protected set; }
        public JobNumber JobNumber { get; protected set; }

        public bool CanUseSkill { get; private set; } = true;
        public int RemainingHealth { get; private set; }
        public bool Fainted { get; private set; }


        protected Character(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth)
        {
            Name = name;
            BaseAttack = baseAttack;
            BaseMagicDefense = baseMagicDefense;
            BasePhysicalDefense = basePhysicalDefense;
            BaseMaximumHealth = baseMaximumHealth;

            assignActualValues();
        }

        protected abstract void UseUniqueAction(Character target);

        public void AttackTarget(Character target)
        {
            target.DefendAgainstAttack(Attack, AttacksPhysical);
        }

        public void TryUseSkill(Character target)
        {
            if(CanUseSkill)
            {
                UseUniqueAction(target);
                CanUseSkill = false;
            }
        }

        public void DefendAgainstAttack(int attackStatAttacker, bool attackerAttacksPhysical)
        {
            int damageToTake = calculateDamageToTake(attackStatAttacker, attackerAttacksPhysical);
            TakeDamage(damageToTake);
        }

        public void TakeDamage(int damageToTake)
        {
            RemainingHealth -= damageToTake;

            if (RemainingHealth < 1)
            {
                Fainted = true;
            }
        }

        public void SetParty(Party party)
        {
            Party = party;
        }

        int calculateDamageToTake(int attackStatAttacker, bool attackerAttacksPhysical)
        {
            int defenseValueToBeUsed;
            if (attackerAttacksPhysical)
                defenseValueToBeUsed = PhysicalDefense;
            else
                defenseValueToBeUsed = MagicDefense;

            return attackStatAttacker - (defenseValueToBeUsed / 2);
        }

        void assignActualValues()
        {
            Attack = BaseAttack * 10;
            MagicDefense = BaseMagicDefense * 10;
            PhysicalDefense = BasePhysicalDefense * 10;
            MaximumHealth = BaseMaximumHealth * 100;
            RemainingHealth = MaximumHealth;
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
