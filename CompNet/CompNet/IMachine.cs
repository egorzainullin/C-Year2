namespace CompNet
{
    public interface IMachine
    {
        string TypeOfOs { get; }

        bool IsInfected { get; }

        void SetInfected();

        double ProbabilityOfInfection { get; }
    }
}