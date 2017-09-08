using System;
using System.Collections;
using System.Collections.Generic;

namespace TreeWithIterator
{
    public class TreeWithIterator<T> : IEnumerable<T>
    where T : IComparable
    {
        private TreeElement root;

        public void Clear()
        {
            root = null;
        }

        public int Count { get; private set; }

        public void Add(T value)
        {
            Add(ref root, value);
        }

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
            DeleteNode(ref this.root, value);
        }

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
                    --this.Count;
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
                    ++this.Count;
                    var nearestNode = node.LeftChild;
                    nearestNode = FindNearest(node, value);
                    node.Value = nearestNode.Value;
                    DeleteNode(ref node.LeftChild, nearestNode.Value);
                    return;
            }
        }

        private TreeElement FindNearest(TreeElement node, T value)
        {
            var nearestNode = node;
            switch (value.CompareTo(root.Value))
            {
                case 0:
                    return root;
                case 1:
                    return FindNearest(root.RightChild, value);
                case -1:
                    return FindNearest(root.LeftChild, value);
                default:
                    return null;
            }
        }

        private class TreeEnumerator : IEnumerator<T>
        {
            private Stack<TreeElement> stack;

            private TreeElement root;

            private bool IsJustStarted;

            public TreeEnumerator(TreeElement root)
            {
                this.root = root;
                Reset();
            }

            public T Current => stack.Peek().Value;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                this.stack = null;
                this.root = null;
            }

            public bool MoveNext()
            {
                if (IsJustStarted)
                {
                    IsJustStarted = false;
                    return true;
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

            public void Reset()
            {
                stack = new Stack<TreeElement>();
                if (root != null)
                {
                    stack.Push(root);
                }
                IsJustStarted = true;
            }
        }

        public IEnumerator<T> GetEnumerator() => new TreeEnumerator(root);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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