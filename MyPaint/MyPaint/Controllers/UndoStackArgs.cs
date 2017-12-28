using System;

namespace MyPaint.Controllers
{
    /// <summary>
    /// This is args of event which thrown when undo stack emptiness changed 
    /// </summary>
    public class UndoStackArgs : EventArgs
    {
        /// <summary>
        /// Is stack empty
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="UndoStackArgs"/> class 
        /// </summary>
        /// <param name="isEmpty">Is this stack is empty</param>
        public UndoStackArgs(bool isEmpty) => this.IsEmpty = isEmpty;
    }
}