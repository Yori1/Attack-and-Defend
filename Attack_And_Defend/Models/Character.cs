using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Attack_And_Defend.Models
{
    public abstract class Character
    {
        [JsonIgnore]
        public Party Party { get; set; }

        public int Id { get; private set; }

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


        public int ExperiencePoints { get; private set; }
        public int TimesFainted { get; private set; }
        public int CharactersDefeated { get; set; }
        public int MatchesWon { get; private set; }

        const int maxTotalBaseValues = 8;

        [JsonConstructor]
        protected Character(int id, string name, int attack, int magicDefense, int physicalDefense, int maximumHealth, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, bool attacksPhysical, JobNumber jobNumber, bool canUseSkill, int remainingHealth, bool fainted, int experiencePoints, int timesFainted, int charactersDefeated, int matchesWon)
        {
            Id = id;
            Name = name;
            Attack = attack;
            MagicDefense = magicDefense;
            PhysicalDefense = physicalDefense;
            MaximumHealth = maximumHealth;
            BaseAttack = baseAttack;
            BaseMagicDefense = baseMagicDefense;
            BasePhysicalDefense = basePhysicalDefense;
            BaseMaximumHealth = baseMaximumHealth;
            AttacksPhysical = attacksPhysical;
            JobNumber = jobNumber;
            CanUseSkill = canUseSkill;
            RemainingHealth = remainingHealth;
            Fainted = fainted;
            ExperiencePoints = experiencePoints;
            TimesFainted = timesFainted;
            CharactersDefeated = charactersDefeated;
            MatchesWon = matchesWon;

            this.ExperiencePoints = experiencePoints;

            ensureValidBaseStats(baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth);
            assignActualValues();
        }

        protected Character(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth,
            int experiencePoints = 0, int timesFainted = 0, int charactersDefeated = 0, int matchesWon = 0)
        {
            Name = name;
            BaseAttack = baseAttack;
            BaseMagicDefense = baseMagicDefense;
            BasePhysicalDefense = basePhysicalDefense;
            BaseMaximumHealth = baseMaximumHealth;

            this.ExperiencePoints = experiencePoints;

            ensureValidBaseStats(baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth);
            assignActualValues();
        }

        protected Character(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, Party party = null,
            int experiencePoints = 0, int timesFainted = 0, int charactersDefeated = 0, int matchesWon = 0)
        {
            Name = name;
            BaseAttack = baseAttack;
            BaseMagicDefense = baseMagicDefense;
            BasePhysicalDefense = basePhysicalDefense;
            BaseMaximumHealth = baseMaximumHealth;
            Party = party;

            this.ExperiencePoints = experiencePoints;

            ensureValidBaseStats(baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth);
            assignActualValues();
        }




        void ensureValidBaseStats(int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth)
        {
            if (baseAttack + baseMagicDefense + basePhysicalDefense + baseMaximumHealth > maxTotalBaseValues)
                throw new ArgumentException("The sum of the base values exceed the limit of " + maxTotalBaseValues + ".");
        }

        void assignActualValues()
        {
            Attack = normalBaseStatToActualStat(BaseAttack);
            MagicDefense = normalBaseStatToActualStat(BaseMagicDefense);
            PhysicalDefense = normalBaseStatToActualStat(BasePhysicalDefense);
            MaximumHealth = 10 + normalBaseStatToActualStat(BaseMaximumHealth) * 5;
            RemainingHealth = MaximumHealth;
        }

        int normalBaseStatToActualStat(int baseStat)
        {
            int level = GetLevel();
            int actualStat = (baseStat * level * 10) / maxTotalBaseValues;
            return actualStat;
        }

        protected abstract void UseUniqueAction(Character target);

        public void TryUseSkill(Character target)
        {
            if (CanUseSkill)
            {
                UseUniqueAction(target);
                CanUseSkill = false;
            }
        }

        public void AttackTarget(Character target)
        {
            if(!target.Fainted)
            {
                target.DefendAgainstAttack(Attack, AttacksPhysical);
                if (target.Fainted)
                {
                    CharactersDefeated++;
                }
            }
        }
        
        public int GetLevel()
        {
            return (ExperiencePoints + 100) / 100;
        }

        #region Defending
        public void DefendAgainstAttack(int attackerAttack, bool attackerAttacksPhysical)
        {
            int damageToTake = calculateDamageToTake(attackerAttack, attackerAttacksPhysical);
            TakeDamage(damageToTake);
        }

        public bool TakeDamage(int damageToTake)
        {
            RemainingHealth -= damageToTake;
            if (RemainingHealth < 0)
                RemainingHealth = 0;

            if (RemainingHealth < 1)
            {
                Fainted = true;
            }
            return Fainted;
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

        #endregion


    }
}
