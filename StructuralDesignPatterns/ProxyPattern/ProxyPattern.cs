using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
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
    public class Property<T> : IEquatable<Property<T>> where T : new()
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


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((Property<T>)obj);

        }

        public bool Equals([AllowNull] Property<T> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            //when all else fails and we can't get a comparison based on the reference equal check
            //we use the default implementation of equals on the type T
            return EqualityComparer<T>.Default.Equals(value, other.value);

        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
            //return EqualityComparer<T>.Default.GetHashCode(value);
        }

        public static bool operator ==(Property<T> left, Property<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Property<T> left, Property<T> right)
        {
            return !Equals(left, right);
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
