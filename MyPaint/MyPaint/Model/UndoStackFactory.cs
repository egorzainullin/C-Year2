using System.Collections.Generic;

namespace MyPaint.Model
{
    public class UndoStackFactory
    {
        private static UndoStack instance;

        public static IUndoStack CreateUndoStack()
        {
            if (instance == null)
            {
                instance = new UndoStack();
            }
            return instance;
        }

        private class UndoStack : IUndoStack
        {
            private Stack<ICommand> undoStack;

            private Stack<ICommand> redoStack;

            public UndoStack()
            {
                this.undoStack = new Stack<ICommand>();
                this.redoStack = new Stack<ICommand>();
            }
            
            public void AddCommand(ICommand command) => undoStack.Push(command);

            public void Undo()
            {
                if (undoStack.Count != 0)
                {
                    var toUndo = undoStack.Pop();
                    redoStack.Push(toUndo);
                    toUndo.Undo();
                }
            }

            public void Redo()
            {
                if (redoStack.Count != 0)
                {
                    var toRedo = redoStack.Pop();
                    undoStack.Push(toRedo);
                    toRedo.Execute();
                }
            }

            public void ResetRedo() => redoStack.Clear();
        }
    }
}
