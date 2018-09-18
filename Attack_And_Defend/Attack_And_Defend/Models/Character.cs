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
        public int Health { get; private set; }
        public bool Fainted { get; private set; }
        public bool AttacksPhysical { get; private set; }

        public Character(string name, int attack, int magicDefense, int health, bool fainted, bool attacksPhysical)
        {
            Name = name;
            Attack = attack;
            MagicDefense = magicDefense;
            Health = health;
            Fainted = fainted;
            AttacksPhysical = attacksPhysical;
        }

        public Character(string name, int attack, int magicDefense, int health, bool attacksPhysical)
        {
            Name = name;
            Attack = attack;
            MagicDefense = magicDefense;
            Health = health;
            AttacksPhysical = attacksPhysical;
        }

        public void AttackTarget(Character target)
        {

        }

        public void TakeDamage(int damageToTake)
        {

        }

        public void UseUniqueAction(Character target)
        { }
    }
}
