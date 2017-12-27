namespace MyPaint.Model
{
    public interface IUndoStack
    {
        void AddCommand(ICommand command);

        void Redo();

        void Undo();

        void ResetRedo();
    }
}