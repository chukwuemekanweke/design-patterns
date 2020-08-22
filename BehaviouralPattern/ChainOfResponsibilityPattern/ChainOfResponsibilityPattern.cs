using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehaviouralPatterns.ChainOfResponsibilityPattern
{
    class ChainOfResponsibilityPattern
    {
    }


    public class Creature
    {
        public string Name { get; set; }
        public int Attack { get; set; } 
        public int Defense {get;set;}

        public Creature(string name, int attack, int defense)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; // linked list

        public CreatureModifier(Creature creature)
        {
            this.creature = creature ?? throw new ArgumentNullException(nameof(creature));
        }

        public void Add(CreatureModifier creatureModifier)
        {
            if (next != null)
                next.Add(creatureModifier);
            else
                next = creatureModifier;
        }

        public virtual void Handle() => next?.Handle();
    }


    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {
        }


        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();  // call's the next modifier in the modifier pipeline
        }
    }


    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Creature creature) : base(creature)
        {
        }


        public override void Handle()
        {
            Console.WriteLine($"Increasing {creature.Name}'s defence");
            creature.Defense += 3;
            base.Handle();  // call's the next modifier in the modifier pipeline
        }
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        {
        }


        public override void Handle()
        {
            Console.WriteLine($"{creature.Name}'s has received divine energy to resist all spells");

        }
    }


}
