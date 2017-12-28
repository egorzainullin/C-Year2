using System;
using System.Collections.Generic;

namespace MyPaint.Controllers
{
    /// <summary>
    /// This class creates undo stack and provides access to it
    /// </summary>
    public class UndoStackFactory
    {
        /// <summary>
        /// Undo stack singleton instance
        /// </summary>
        private static UndoStack instance;

        /// <summary>
        /// Creates undo stack
        /// </summary>
        /// <returns>Created undo stack</returns>
        public static IUndoStack GetUndoStack()
        {
            if (instance == null)
            {
                instance = new UndoStack();
            }
            return instance;
        }

        /// <summary>
        /// This class implements UndoStack interface
        /// </summary>
        private class UndoStack : IUndoStack
        {
            /// <summary>
            /// Undo stack
            /// </summary>
            private Stack<ICommand> undoStack;

            /// <summary>
            /// Redo stack
            /// </summary>
            private Stack<ICommand> redoStack;

            /// <summary>
            /// This event happens when undo stack emptiness has changed
            /// </summary>
            public event EventHandler<UndoStackArgs> UndoAvailibleChanged;

            /// <summary>
            /// Throw UndoAvailibleChanged event
            /// </summary>
            /// <param name="isEmpty">Is stack empty</param>
            private void ThrowUndoChanged(bool isEmpty) => UndoAvailibleChanged?.Invoke(this, new UndoStackArgs(isEmpty));

            /// <summary>
            /// This event happens when redo stack emptiness has changed
            /// </summary>
            public event EventHandler<UndoStackArgs> RedoAvailibleChanged;

            /// <summary>
            /// Throw RedoAvailibleChanged event
            /// </summary>
            /// <param name="isEmpty">Is stack empty</param>
            private void ThrowRedoChanged(bool isEmpty) => RedoAvailibleChanged?.Invoke(this, new UndoStackArgs(isEmpty));

            /// <summary>
            /// Initializes a new instance of <see cref="UndoStack"/> class
            /// </summary>
            public UndoStack()
            {
                this.undoStack = new Stack<ICommand>();
                this.redoStack = new Stack<ICommand>();
            }

            /// <summary>
            /// Adds command to undo stack
            /// </summary>
            /// <param name="command">Command to add</param>
            public void AddCommand(ICommand command)
            {
                undoStack.Push(command);
                ThrowUndoChanged(false);
            }

            /// <summary>
            /// Undoes lat command
            /// </summary>
            public void Undo()
            {
                if (undoStack.Count != 0)
                {
                    var toUndo = undoStack.Pop();
                    redoStack.Push(toUndo);
                    ThrowRedoChanged(false);
                    toUndo.Undo();
                }
                if (undoStack.Count == 0)
                {
                    ThrowUndoChanged(true);
                }
            }

            /// <summary>
            /// Redoes last undone command
            /// </summary>
            public void Redo()
            {
                if (redoStack.Count != 0)
                {
                    var toRedo = redoStack.Pop();
                    undoStack.Push(toRedo);
                    ThrowUndoChanged(false);
                    toRedo.Execute();
                }
                if (redoStack.Count == 0)
                {
                    ThrowRedoChanged(true);
                }
            }

            /// <summary>
            /// Resets redo stack
            /// </summary>
            public void ResetRedo()
            {
                redoStack.Clear();
                ThrowRedoChanged(true);
            }
        }
    }
}
