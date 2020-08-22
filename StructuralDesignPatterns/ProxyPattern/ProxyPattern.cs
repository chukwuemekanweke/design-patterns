using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralDesignPatterns.ProxyPattern
{
    class ProxyPattern
    {
    }



    #region Protection Proxy

    public interface ICar
    {
        void Drive();
    }


    public class Driver
    {
        public int Age { get; set; }

        public Driver(int age)
        {
            Age = age;
        }
    }

    public class Car : ICar
    {
        private Driver driver;
        public void Drive()
        {
            Console.WriteLine("Car is being driven");
        }
    }

    public class CarProxy : ICar
    {
        private Driver driver;
        private Car car = new Car();

        public CarProxy(Driver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void Drive()
        {
            if (driver.Age >= 16)
                car.Drive();
            else
                Console.WriteLine("Too young");


        }
    }

    #endregion


    #region Property Proxy
    public class Property<T> where T : new()
    {
        private T value;

        public T Value
        {
            get => value;
            set
            {
                if (Equals(this.value, value))
                    return;

                Console.WriteLine($"Assigning value to {value}");
                this.value = value;
            }
            
        }

        public Property() : this(Activator.CreateInstance<T>())
        {

        }

        public Property(T value)
        {
            this.value = value;
        }

        public static implicit operator T(Property<T> property)
        {
            return property.value;    // int n = p_int;
        }



        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); //Property<int> p = 123
        }



    }

    public class Creature
    {
        private Property<int> agility { get; set; } = new Property<int>();

        public int Agility
        {
            get => agility.Value;
            set => agility.Value = value;
        }
    }
    #endregion


}
