namespace MyPaint.Model
{
    public interface ICommand
    {
        void Undo();
        void Execute();
    }
}