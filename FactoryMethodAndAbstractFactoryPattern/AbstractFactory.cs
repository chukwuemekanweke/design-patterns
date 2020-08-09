using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.FactoryMethodAndAbstractFactoryPattern
{
    public class AbstractFactory
    {
    }


    #region ABstract Factory
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice, but i'd prefer it with milk");
        }
    }

    internal class Coffe : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffe is sensational!");
        }
    }


    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }





    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal class CoffeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar.");
            return new Coffe();
        }
    }

    #region Abstract Factory That Doesn't Follow Open Closed Principle
    public class HotDrinkMachine
    {
        public enum AvailableDrink
        {
            Coffe, Tea
        }


        private Dictionary<AvailableDrink, IHotDrinkFactory> Factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();


        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                //Get's an instance of the interface implementation class using reflection and creates an instance of the class using the Activator
                IHotDrinkFactory factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType("DesignPatterns.FactoryMethodAndAbstractFactoryPattern." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
                Factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return Factories[drink].Prepare(amount);
        }

    }
    #endregion


    #region Abstract Factory That Follows Open Closed Principle
    public class HotDrinkMachineOCP
    {

        private List<Tuple<string, IHotDrinkFactory>> Factories = new List<Tuple<string, IHotDrinkFactory>>();
        public HotDrinkMachineOCP()
        {
            Type[] typesDefinedInThisAssembly = typeof(HotDrinkMachine).Assembly.GetTypes();// returns the class types that are defined in the same assembly where the HotDrinkMachine class is located
            foreach (var type in typesDefinedInThisAssembly)
            {
                /*
                 *  checks if the type under consideration implements the interface
                 * */
                bool doesTypeImplementInterface =  typeof(IHotDrinkFactory).IsAssignableFrom(type);

                /*
                 Ensures we're not getting an interface type. 
                 */
                bool isNotInterface = !type.IsInterface;

                if(doesTypeImplementInterface && isNotInterface)
                {
                    Tuple<string,IHotDrinkFactory>  entry = Tuple.Create(type.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(type));
                    Factories.Add(entry);
                }

            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available Drinks");

            for (int i = 0; i < Factories.Count; i++)
            {
                Tuple<string, IHotDrinkFactory> entry = Factories[i];
                Console.WriteLine($"{i}: {entry.Item1}");
                
            }

            while (true)
            {
                string input = Console.ReadLine();

                if(int.TryParse(input, out int drinkOption) && drinkOption>=0 && drinkOption <= Factories.Count)
                {
                    Console.WriteLine("Specify Amount");

                    input = Console.ReadLine();

                    if (int.TryParse(input, out int amount) && amount > 0 )
                    {
                       IHotDrinkFactory hotDrinkFactory =   Factories[drinkOption].Item2;
                       return  hotDrinkFactory.Prepare(amount);
                    }

                }
                else
                {
                    Console.WriteLine("Selected option is invalid");
                }

            }
        }
    }
    #endregion


    #endregion
}
