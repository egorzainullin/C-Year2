namespace MyPaint.Model
{
    /// <summary>
    /// This interface provides support of canceling and redoing operations
    /// </summary>
    public interface IUndoStack
    {
        /// <summary>
        /// Adds a new command to undo stack
        /// </summary>
        /// <param name="command">Command to add</param>
        void AddCommand(ICommand command);

        /// <summary>
        /// Redoes last command
        /// </summary>
        void Redo();

        /// <summary>
        /// Undoes last command
        /// </summary>
        void Undo();

        /// <summary>
        /// Resets redo stack
        /// </summary>
        void ResetRedo();
    }
}