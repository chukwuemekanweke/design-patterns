using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace DesignPatterns.MediatorPattern
{
    class MediatorPattern
    {
    }


    public class Person
    {
        public string Name;
        public ChatRoom Room;
        private List<string> chatLog = new List<string>();

        public Person(string name)
        {
            Name = name;
        }

        public void Say(string message)
        {
            Room.BroadCast(Name, message);
        }

        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);
        }

        public void Receive(string sender, string message)
        {
            string s = $"{sender}: '{message}'";
            chatLog.Add(s);
            Console.WriteLine($"[{Name}'s chat session]: {s}");
        }
    }

    public class ChatRoom
    {
        private List<Person> people = new List<Person>();

        public void Join(Person person)
        {
            string joinMsg = $"{person.Name} joins the chat";

            person.Room = this;
            people.Add(person);

            BroadCast("room", joinMsg);
        }

        public void BroadCast(string source, string message)
        {
            foreach (var person in people)
            {
                if (!string.Equals(person.Name, source))
                {
                    person.Receive(source, message);
                }
            }
        }

        public void Message(string source, string destination, string message)
        {
            people.FirstOrDefault(p => p.Name == destination)?.Receive(source, message);
        }
    }







    public class Actor
    {
        protected EventBroker broker;

        public Actor(EventBroker broker)
        {
            this.broker = broker ?? throw new ArgumentNullException(nameof(broker));
        }
    }

    public class FootballPlayer : Actor
    {
        public string Name { get; set; }
        public int GoalsScored { get; set; } = 0;


        public void Score()
        {
            GoalsScored++;
            broker.Publish(new PlayerScoredEvent { Name = Name, GoalsScored = GoalsScored });
        }

        public void AssaultReferee()
        {
            broker.Publish(new PlayerSentOffEvent { Name = Name, Reason = "violence" });
        }

        public FootballPlayer(EventBroker broker, string name) : base(broker)
        {
            Name = name;

            broker.OfType<PlayerScoredEvent>().Where(ps=> !ps.Name.Equals(name)).Subscribe(pe => {

                if (pe.GoalsScored < 3)
                    Console.WriteLine($"{name} : Nicely done, {pe.Name}! It's your {pe.GoalsScored} goal");

            });

            broker.OfType<PlayerSentOffEvent>().Where(ps => !ps.Name.Equals(name)).Subscribe(pe => {

                if (pe.Reason == "violence")
                    Console.WriteLine($"{name}: See you in the lockers, {pe.Name}.");

            });

        }
    }

    public class FootballCoach : Actor
    {
        public FootballCoach(EventBroker broker) : base(broker)
        {

            broker.OfType<PlayerScoredEvent>().Subscribe(pe => {

                if (pe.GoalsScored < 3)
                    Console.WriteLine($"Coach: well done, {pe.Name}!");

            });

            broker.OfType<PlayerSentOffEvent>().Subscribe(pe => {

                if (pe.Reason=="violence")
                    Console.WriteLine($"Coach: How could you, {pe.Name}.");

            });
        }


    }

    public class PlayerEvent
    {
        public string Name { get; set; }
    }

    public class PlayerScoredEvent: PlayerEvent
    {
        public int GoalsScored { get; set; }
    }

    public class PlayerSentOffEvent: PlayerEvent
    { 
      public string Reason { get; set; }
    }

    public class EventBroker : IObservable<PlayerEvent>
    {

        Subject<PlayerEvent> subscriptions = new Subject<PlayerEvent>();

        public IDisposable Subscribe(IObserver<PlayerEvent> observer)
        {
            return subscriptions.Subscribe(observer);
        }


        public void Publish(PlayerEvent @event)
        {

            subscriptions.OnNext(@event);

        }

    }

   


}
