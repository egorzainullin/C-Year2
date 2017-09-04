using System;
using System.Collections;
using System.Collections.Generic;

namespace TreeWithIterator
{
    public class TreeWithIterator<T> : IEnumerable<T>
    where T : IComparable
    {
        private TreeElement root;

        public void Print()
        {
            Print(root);
            void Print(TreeElement root)
            {
                if (root == null)
                {
                    return;
                }
                Console.WriteLine(root.Value);
                Print(root.LeftChild);
                Print(root.RightChild);
            }
        }

        public void Add(T value)
        {
            Add(ref root, value);
          
        }

          void Add(ref TreeElement root, T value)
            {
               if (root == null)
               {
                   root = new TreeElement(null, null, value);
                   return;
               }
            //    if (root.LeftChild == null && value.CompareTo(root.Value) == -1)
            //    {
            //        var newElement = new TreeElement(null, null, value);
            //        root.LeftChild = newElement;
            //    }
            //    if (root.RightChild == null && value.CompareTo(root.Value) == 1)
            //    {
            //        var newElement = new TreeElement(null, null, value);
            //        root.RightChild = newElement;
            //    }
               switch(value.CompareTo(root.Value))
               {
                   case 0: 
                   return;
                   case 1:
                   Add(ref root.RightChild, value);
                   break;
                   case -1:
                   Add(ref root.LeftChild, value);
                   break;
               }
            }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class TreeElement
        {
            public TreeElement LeftChild;

            public TreeElement RightChild;

            public T Value;

            public TreeElement(TreeElement leftChild, TreeElement rightChild, T value)
            {
                this.LeftChild = leftChild;
                this.RightChild = rightChild;
                this.Value = value;
            }
        }
    }
}