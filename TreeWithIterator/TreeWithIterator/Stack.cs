using System;

namespace StackList
{
    /// <summary>
    /// Class Stack
    /// </summary>
    public class SimpleStack<T>
    {
        /// <summary>
        /// Stack's element count
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Element of stack
        /// </summary>
        private class StackElement
        {
            /// <summary>
            /// Next element
            /// </summary>
            public StackElement Next => next;

            /// <summary>
            /// Helper field for next element
            /// </summary>
            private StackElement next;

            /// <summary>
            /// Value of element
            /// </summary>
            public T Value => value;

            /// <summary>
            /// Helper field for value
            /// </summary>
            private T value;

            /// <summary>
            /// Initializes an instance of <see cref="StackElement"/>
            /// </summary>
            /// <param name="next">Reference for next element</param>
            /// <param name="value">Value of element of stack</param>
            public StackElement(StackElement next, T value)
            {
                this.next = next;
                this.value = value;
            }
        }

        /// <summary>
        /// Head of stack
        /// </summary>
        private StackElement head;

        /// <summary>
        /// Pushes value into stack
        /// </summary>
        /// <param name="value">Value to push</param>
        public void Push(T value)
        {
            StackElement newElement = new StackElement(head, value);
            head = newElement;
            ++Count;
        }

        /// <summary>
        /// Pops value from stack
        /// </summary>
        /// <exception cref="InvalidOperationException">Trying to get value from empty stack</exception>
        public T Pop()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Attempt to pop value from empty stack");
            }
            T value = head.Value;
            head = head.Next;
            --Count;
            return value;
        }

        /// <summary>
        /// Peeks value from stack 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Trying to get value from empty stack</exception>
        public T Peek()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Attempt to peek value from empty stack");
            }
            return head.Value;
        }

        /// <summary>
        /// Is this stack has no elements
        /// </summary>
        /// <returns>true, if empty, else - false</returns>
        public bool IsEmpty() => head == null;

        /// <summary>
        /// Clears stack
        /// </summary>
        public void Clear()
        {
            head = null;
            Count = 0;
        }
    }
}
