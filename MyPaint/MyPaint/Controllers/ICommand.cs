namespace MyPaint.Controllers
{
    /// <summary>
    /// This is class that provides command's base interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Undo last command
        /// </summary>
        void Undo();

        /// <summary>
        /// Redo last undone command
        /// </summary>
        void Execute();
    }
}