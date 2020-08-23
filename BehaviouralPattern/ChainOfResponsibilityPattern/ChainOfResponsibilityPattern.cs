using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehaviouralPatterns.ChainOfResponsibilityPattern
{
    class ChainOfResponsibilityPattern
    {
    }

    #region Classic Chain Of Responsibility
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
    #endregion



    #region Broker Chain Implementation With Mediator

    /// <summary>
    /// Mediator -- Event Broker
    /// </summary>
    public class Game
    {
        public event EventHandler<Query> Queries;

        /// <summary>
        ///  Invokes the handlers that have been assigned to this event
        ///  For the first modifer, the handle method of that modifier is assigned as an event handler
        ///  For the second modifier, same thing
        ///  
        /// When the perform query method is called in the accessor of the Attack and Defense properties
        /// of the Creature2 . all the registered handlers get called and make appropriate changes on the value field of the Query
        /// 
        /// The original Creature2 object attack and defense values are never modified however. which is totally cool
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="q"></param>
        public void PerformQuery(object sender, Query q)
        {
            Queries?.Invoke(sender, q);  
        }
    }

    public class Query
    {
        public string CreatureName;

        public Argument WhatToQuery;
        public int Value;

        public Query(string creatureName, Argument whatToQuery, int value)
        {
            CreatureName = creatureName ?? throw new ArgumentNullException(nameof(creatureName));
            WhatToQuery = whatToQuery;
            Value = value;
        }
    }

    public enum Argument
    {
        Attack,Defense
    }


    public class Creature2
    {
        private Game game;
        public string Name;
        private int attack, defense;

        public Creature2(Game game, string name, int attack, int defense)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            this.attack = attack;
            this.defense = defense;
        }

        /// <summary>
        /// Anytime the Attack property is accessed, a new Query object is created,
        /// once that's done the queries event handler is invoked so all registered handlers for that event get triggered 
        /// </summary>
        public int Attack
        {
            get
            {
                var q = new Query(Name, Argument.Attack, attack);
                game.PerformQuery(this, q); //q.value
                return q.Value;
            }
        }

        /// <summary>
        /// Anytime the Defence property is accessed, a new Query object is created,
        /// once that's done the queries event handler is invoked so all registered handlers for that event get triggered 
        /// </summary>
        public int Defense
        {
            get
            {
                var q = new Query(Name, Argument.Defense, defense);
                game.PerformQuery(this, q); //q.value
                return q.Value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public abstract class CreatureModifier2 : IDisposable
    {
        protected Game game;
        protected Creature2 Creature;

        /// <summary>
        /// the abstract handle method of this class get's assigned to the game Queries event
        /// </summary>
        /// <param name="game"></param>
        /// <param name="creature"></param>
        protected CreatureModifier2(Game game, Creature2 creature)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            Creature = creature ?? throw new ArgumentNullException(nameof(creature));
            game.Queries += Handle;
        }

        protected abstract void Handle(object sender, Query q);

        public void Dispose()
        {
            game.Queries -= Handle; 
        }


    }


    public class DoubleAttackModifier2 : CreatureModifier2
    {
        public DoubleAttackModifier2(Game game, Creature2 creature) : base(game, creature)
        {
        }

        /// <summary>
        /// implementation of the abstract Handle event handler for this modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="q"></param>
        protected override void Handle(object sender, Query q)
        {
            if(q.CreatureName== Creature.Name && q.WhatToQuery== Argument.Attack)
            {
                q.Value *= 2;
            }
        }
    }        

     public class IncreaseDefenseModifier2 : CreatureModifier2
    {
        public IncreaseDefenseModifier2(Game game, Creature2 creature) : base(game, creature)
        {
        }

        /// <summary>
        /// implementation of the abstract Handle event handler for this modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="q"></param>
        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == Creature.Name && q.WhatToQuery == Argument.Defense)
            {
                q.Value += 3;
            }
        }
    }


    #endregion


}

  