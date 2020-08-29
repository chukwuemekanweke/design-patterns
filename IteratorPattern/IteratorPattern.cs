using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.IteratorPattern
{
    class IteratorPattern
    {
    }



    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right) : this(value)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));

            left.Parent = right.Parent = this;
        }





    }


    public class InOrderIterator<T>
    {
        private readonly Node<T> root;
        public Node<T> Current { get; set; }
        private bool yieldedStart;

        public InOrderIterator(Node<T> root)
        {
            this.root = root ?? throw new ArgumentNullException(nameof(root));
            Current = root;

            while (Current.Left != null)
            {
                Current = Current.Left;
            }

            //   1   <- root
            //  / \
            //2     3
            //^ Current
        }


        /// <summary>
        /// the .NET framework checks for this method as well as the Current property for the iterator to be valid
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (!yieldedStart)
            {
                yieldedStart = true;
                return true;
            }

            if (Current.Right != null)
            {
                Current = Current.Right;

                while (Current.Left != null)
                    Current = Current.Left;
                return true;
            }
            else   // if we can't find an element at the right node, we go back to the root
            {
                var parent = Current.Parent;
                while(parent!=null && Current == parent.Right)
                {
                    Current = parent;
                    parent = parent.Parent; 

                }
                Current = parent;
                return Current!=null;
            }
        }

        public void Reset()
        {
            Current = root;
            yieldedStart = false;
        }

    }






    public class BinaryTree<T>
    {
        private Node<T> root;

        public BinaryTree(Node<T> root)
        {
            this.root = root;
        }

        /// <summary>
        /// this makes it possible for you to do a foreach on the tree
        /// </summary>
        /// <returns></returns>
        public InOrderIterator<T> GetEnumerator()
        {
            return new InOrderIterator<T>(root);
        }

        public IEnumerable<Node<T>> InOrder
        {
            get
            {
                IEnumerable<Node<T>> Traverse(Node<T> current)
                {
                    if (current.Left != null)
                    {
                        foreach (var left in Traverse(current.Left) )
                        {
                            yield return left;
                        }
                    }
                    yield return current;

                    if (current.Right != null)
                    {
                        foreach (var right in Traverse(current.Right))
                        {
                            yield return right;
                        }
                    }
                }

                foreach (var node in Traverse(root))
                {
                    yield return node;
                }


            }
        }
    }


    /// <summary>
    /// Array backed properties
    /// </summary>
    public class IterateCreature : IEnumerable<int>
    {

        private int[] stats = new int[3];

        private const int strength = 0;
        private const int agility = 0;
        private const int intelligence = 0;

        public int Strength { get => stats[strength]; set => stats[strength] = value; }
        public int Agility { get => stats[agility]; set => stats[agility] = value; }
        public int Intelligence { get => stats[intelligence]; set => stats[intelligence] = value; }

        public double AverageStat
        {
            get { return stats.Average(); }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

         public int this[int index]
        {
            get { return stats[index]; }
            set { stats[index] = value; }
        }



    }

}
