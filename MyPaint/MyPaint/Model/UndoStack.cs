using System.Collections.Generic;

namespace MyPaint.Model
{
    public class UndoStack
    {
        private Stack<ICommand> undoStack;

        private Stack<ICommand> redoStack;

        private static UndoStack instance;

        private UndoStack()
        {
            this.undoStack = new Stack<ICommand>();
            this.redoStack = new Stack<ICommand>();
        }

        public static UndoStack CreateUndoStack() => instance ?? new UndoStack();

        public void AddCommand(ICommand command) => undoStack.Push(command);

        public void Undo()
        {
            var toUndo = undoStack.Pop();
            redoStack.Push(toUndo);
            toUndo.Undo();
        }

        public void Redo()
        {
            var toRedo = redoStack.Pop();
            undoStack.Push(toRedo);
            toRedo.Execute();
        }
    }
}
