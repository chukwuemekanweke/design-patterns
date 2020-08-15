using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DesignPatterns.StructuralDesignPatterns.CompositeDesignPattern
{
    public class CompositeDesignPattern
    { }

    #region Composite Pattern 1
    public class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";
        public string Color;


        private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();

        public List<GraphicObject> Children => children.Value;

        private StringBuilder Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth))
                .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color}")
                .AppendLine(Name);

            foreach (var child in Children)
            {
                sb = child.Print(sb, depth + 1);
            }
            return sb;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder = Print(stringBuilder, 0);
            return stringBuilder.ToString();
        }
    }

    public class Circle : GraphicObject
    {
        public override string Name { get => "Cirlce"; }
    }

    public class Square : GraphicObject
    {
        public override string Name { get => "Square"; }

    }
    #endregion

    #region Composite Patern 2

    public static class ExtensionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            //Determines whether the specified object instances are the same thing
            if (ReferenceEquals(self, other))
                return;

            foreach (var from in self)
            {
                foreach (var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }

    public class Neuron: IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;

        
        /// <summary>
        /// an single object can masquerade as an enumerable with the "yield return this" statement
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class NeuronLayer: Collection<Neuron>
    {

    }

    public class NeuronRing : List<Neuron>
    {

    }

    #endregion



}
