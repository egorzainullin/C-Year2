namespace MyPaint.Model
{
    /// <summary>
    /// This is class that provides command's base interface
    /// </summary>
    public interface ICommand
    {
        void Undo();

        void Execute();
    }
}