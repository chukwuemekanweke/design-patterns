using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.SOLIDDesignPrinciple
{
    /// <summary>
    /// High level parts of a system should not depend on low level parts of a system directly. 
    ///    Use abstractions
    /// </summary>
    class DependencyInversionPrinciple
    {
    }


    public enum Relationship
    {
        Parent, Child, Sibling
    }

    public class Person
    {
        public string Name;

    }


    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildOf(string parentName);
    }


    //low-level

    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> Relations { get; } = new List<(Person, Relationship, Person)>();


        public void AddParentAndChild(Person parent, Person child)
        {
            Relations.Add((parent, Relationship.Parent, child));
            Relations.Add((child, Relationship.Child, parent));

        }

        public IEnumerable<Person> FindAllChildOf(string parentName)
        {
            var relations = Relations;

            foreach (var relation in relations.Where(x => x.Item1.Name == parentName && x.Item2 == Relationship.Parent))
            {
                yield return relation.Item3;
            }

        }

    }


    public class Research
    {

        public Research(IRelationshipBrowser browser)
        {
            foreach (Person child in browser.FindAllChildOf("John"))
            {
                Console.WriteLine($"John has a child called {child.Name}");

            }
        }

        #region Anti Pattern
        //public Research(Relationships relationships)
        //{

        //   var relations = relationships.Relations;

        //    foreach (var relation in relations.Where(x=>x.Item1.Name == "John" && x.Item2== Relationship.Parent))
        //    {
        //        Console.WriteLine($"John has a child called {relation.Item3}");
        //    }

        //}


        #endregion
    }

}


