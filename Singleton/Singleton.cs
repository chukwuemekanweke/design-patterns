using MoreLinq.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DesignPatterns.Singleton
{
    public class Singleton
    {
    }


    #region Safe Singleton Pattern
    public interface IDatabase
    {
        int GetPopulation(string name);
    }


    public class SingletonDatabase : IDatabase
    {

        private Dictionary<string, int> capitals;

        public SingletonDatabase()
        {
            Console.WriteLine("Initializing Database");

            string fileName = Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "Capitals.txt");

            capitals = File.ReadAllLines(fileName).Batch(2).ToDictionary(list => list.ElementAt(0).Trim(), list => int.Parse(list.ElementAt(1)));

        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }


        /*
         
            This Lazy feature in c# looks nice

            Lazy initialization of an object means that its creation is deferred until it is first used. 
            (For this topic, the terms lazy initialization and lazy instantiation are synonymous.) 
            Lazy initialization is primarily used to improve performance, avoid wasteful computation, 
            and reduce program memory requirements. These are the most common scenarios:

             If no delegate is passed in the Lazy<T> constructor, the wrapped type is created by using Activator.
             CreateInstance when the value property is first accessed. 
             If the type does not have a parameterless constructor, a run-time exception is thrown.

            A Lazy<T> object always returns the same object or value that it was initialized with. 
            Therefore, the Value property is read-only. 
            If Value stores a reference type, you cannot assign a new object to it. 
            (However, you can change the value of its settable public fields and properties.) 
            If Value stores a value type, you cannot modify its value. 
            Nevertheless, you can create a new variable by invoking the variable constructor again by using new arguments.
            
            By default, Lazy<T> objects are thread-safe.
            That is, if the constructor does not specify the kind of thread safety, 
            the Lazy<T> objects it creates are thread-safe.
            In multi-threaded scenarios, the first thread to access the Value property of a thread-safe Lazy<T> object initializes it for all subsequent accesses on all threads, and all threads share the same data. 
            Therefore, it does not matter which thread initializes the object, and race conditions are benign.
             * 
             */
        private static Lazy< SingletonDatabase> instance = new Lazy<SingletonDatabase>(()=> new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }
    #endregion


    #region MonoState Singleton Pattern  [It exists, but never use it please]
    public class CEO
    {
        private static string name;
        private static int age;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }



    }
    #endregion



}
