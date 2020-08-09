using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.FactoryMethodAndAbstractFactoryPattern
{
    public class AbstractFactory
    {
    }



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


    public class HotDrinkMachine
    {
        public enum AvailableDrink
        {
           Coffe,Tea
        }


        private Dictionary<AvailableDrink, IHotDrinkFactory> Factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();


        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {

                IHotDrinkFactory factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType("DesignPatterns.FactoryMethodAndAbstractFactoryPattern." + Enum.GetName(typeof(AvailableDrink),drink)+"Factory"));
                Factories.Add(drink,factory);
            }    
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return Factories[drink].Prepare(amount);  
        }

    }

}                                 
