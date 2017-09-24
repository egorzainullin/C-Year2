using System;
using System.Collections;
using System.Collections.Generic;
using StackList;

namespace TreeWithIterator
{
    /// <summary>
    /// Collection which saves elements in binary tree
    /// </summary>
    public class TreeWithIterator<T> : IEnumerable<T>
    where T : IComparable
    {
        /// <summary>
        /// Root of tree
        /// </summary>
        private TreeElement root;

        /// <summary>
        /// Clears tree
        /// </summary>
        public void Clear()
        {
            this.Count = 0;
            root = null;
        }

        /// <summary>
        /// Count of elements
        /// </summary>
        /// <returns>Count</returns>
        public int Count { get; private set; }

        /// <summary>
        /// Adds value to collection
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            Add(ref root, value);
        }

        /// <summary>
        /// Method helper which adds value to collection
        /// </summary>
        /// <param name="root">Some node from which adding starts</param>
        /// <param name="value">Value to add</param>
        private void Add(ref TreeElement root, T value)
        {
            if (root == null)
            {
                root = new TreeElement(null, null, value);
                ++this.Count;
                return;
            }
            switch (value.CompareTo(root.Value))
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

        /// <summary>
        /// Searchs for value in collection
        /// </summary>
        /// <param name="value">Value to search</param>
        /// <returns>Reference to element, if found, else - null</returns>
        private TreeElement SearchFor(T value)
        {
            return SearchFor(root);
            TreeElement SearchFor(TreeElement root)
            {
                if (root == null)
                {
                    return null;
                }
                switch (value.CompareTo(root.Value))
                {
                    case 0:
                        return root;
                    case 1:
                        return SearchFor(root.RightChild);
                    case -1:
                        return SearchFor(root.LeftChild);
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Is value in collection
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True, if it is contained, else - false</returns>
        public bool IsContained(T value) => SearchFor(value) != null;

        /// <summary>
        /// Removes value from collection
        /// </summary>
        /// <param name="value">Value to remove</param>
        public void Remove(T value)
        {
            var valueToRemove = SearchFor(value);
            if (valueToRemove == null)
            {
                return;
            }
            --this.Count;
            DeleteNode(ref this.root, value);
        }

        /// <summary>
        /// Method helper which deletes 
        /// </summary>
        /// <param name="node">Node from which we started</param>
        /// <param name="value">Value to delete</param>
        private void DeleteNode(ref TreeElement node, T value)
        {
            if (node == null)
            {
                return;
            }
            switch (value.CompareTo(node.Value))
            {
                case -1:
                    DeleteNode(ref node.LeftChild, value);
                    break;
                case 1:
                    DeleteNode(ref node.RightChild, value);
                    break;
                case 0:
                    if (node.LeftChild == null)
                    {
                        node = node.RightChild;
                        return;
                    }
                    if (node.RightChild == null)
                    {
                        node = node.RightChild;
                        return;
                    }
                    var suitableValue = FindNearest(ref node.LeftChild, value);
                    node.Value = suitableValue;
                    return;
            }
        }

        /// <summary>
        /// Finds nearest value 
        /// </summary>
        /// <param name="node">Node from which we start</param>
        /// <param name="value">Value to find</param>
        /// <returns>Value is near value to delete</returns>
        private T FindNearest(ref TreeElement node, T value)
        {

            if (node.RightChild == null)
            {
                var temp = node.Value;
                if (node.LeftChild == null)
                {
                    node = null;
                }
                else
                {
                    node = node.LeftChild;
                }
                return temp;
            }
            else
            {
                return FindNearest(ref node.RightChild, value);
            }
        }

        /// <summary>
        /// Enumerator of this collection
        /// </summary>
        private class TreeEnumerator : IEnumerator<T>
        {
            /// <summary>
            /// Stack to keep nodes
            /// </summary>
            private SimpleStack<TreeElement> stack;

            /// <summary>
            /// Root of tree
            /// </summary>
            private TreeElement root;

            /// <summary>
            /// Collection hasn't been enumerated yet
            /// </summary>
            private bool IsJustStarted;

            /// <summary>
            /// Initializes an instance of <see cref="TreeEnumerator"/>
            /// </summary>
            /// <param name="root">Root of tree to enumerate</param>
            public TreeEnumerator(TreeElement root)
            {
                this.root = root;
                Reset();
            }

            /// <summary>
            /// Current element of collection that is pointed by enumerator
            /// </summary>
            /// <returns>Current element</returns>
            public T Current => stack.Peek().Value;

            /// <summary>
            /// Current element of collection that is pointed by enumerator
            /// </summary>
            /// <returns>Current element</returns>
            object IEnumerator.Current => Current;

            /// <summary>
            /// Disposes this enumerator
            /// </summary>
            public void Dispose()
            {
                this.stack = null;
                this.root = null;
            }

            /// <summary>
            /// Sets pointer to next element
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                if (IsJustStarted)
                {
                    IsJustStarted = false;
                    return root != null;
                }
                if (stack.Count == 0)
                {
                    return false;
                }
                var poppedNode = stack.Pop();
                if (poppedNode.LeftChild != null)
                {
                    stack.Push(poppedNode.LeftChild);
                }
                if (poppedNode.RightChild != null)
                {
                    stack.Push(poppedNode.RightChild);
                }
                return stack.Count != 0;
            }

            /// <summary>
            /// Resets enumerator to start of collection
            /// </summary>
            public void Reset()
            {
                stack = new SimpleStack<TreeElement>();
                if (root != null)
                {
                    stack.Push(root);
                }
                IsJustStarted = true;
            }
        }

        /// <summary>
        /// Returns the enumerator
        /// </summary>
        /// <returns>Enumerator of the collection</returns>
        public IEnumerator<T> GetEnumerator() => new TreeEnumerator(root);

        /// <summary>
        /// Returns the enumerator
        /// </summary>
        /// <returns>Enumerator of the collection</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Class thar realizes node of tree
        /// </summary>
        private class TreeElement
        {
            /// <summary>
            /// Left child of this node
            /// </summary>
            public TreeElement LeftChild;

            /// <summary>
            /// Right child of this node
            /// </summary>
            public TreeElement RightChild;

            /// <summary>
            /// Value
            /// </summary>
            public T Value;

            /// <summary>
            /// Initializes an instance of <see cref="TreeElement"/>
            /// </summary>
            /// <param name="leftChild"></param>
            /// <param name="rightChild"></param>
            /// <param name="value"></param>
            public TreeElement(TreeElement leftChild, TreeElement rightChild, T value)
            {
                this.LeftChild = leftChild;
                this.RightChild = rightChild;
                this.Value = value;
            }
        }
    }
}