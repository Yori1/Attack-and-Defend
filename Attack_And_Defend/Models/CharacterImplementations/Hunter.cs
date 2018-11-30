using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Attack_And_Defend.Models
{
    public class Hunter: Character
    {
        public Hunter(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, Party party = null, int experiencePoints = 0,
            int timesFainted = 0, int charactersDefeated = 0, int matchesWon = 0)
            : base(name, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth, party, experiencePoints, timesFainted, charactersDefeated, matchesWon)
        {
            this.JobNumber = JobNumber.Hunter;
            this.AttacksPhysical = true;
        }

        public Hunter(string name, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth,
           int experiencePoints, int timesFainted, int charactersDefeated, int matchesWon):base(name, baseAttack, baseMagicDefense, basePhysicalDefense,
               baseMaximumHealth, experiencePoints, timesFainted, charactersDefeated, matchesWon)
        {
            this.JobNumber = JobNumber.Hunter;
            this.AttacksPhysical = true;
        }

        [JsonConstructor]
        public Hunter(int id, string name, int attack, int magicDefense, int physicalDefense, int maximumHealth, int baseAttack, int baseMagicDefense, int basePhysicalDefense, int baseMaximumHealth, bool attacksPhysical, JobNumber jobNumber, bool canUseSkill, int remainingHealth, bool fainted, int experiencePoints, int timesFainted, int charactersDefeated, int matchesWon) : base(id, name, attack, magicDefense, physicalDefense, maximumHealth, baseAttack, baseMagicDefense, basePhysicalDefense, baseMaximumHealth, attacksPhysical, jobNumber, canUseSkill, remainingHealth, fainted, experiencePoints, timesFainted, charactersDefeated, matchesWon)
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
