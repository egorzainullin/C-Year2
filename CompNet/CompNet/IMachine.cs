namespace CompNet
{
    public interface IMachine
    {
        /// <summary>
        /// Type of OS
        /// </summary>
        string TypeOfOS { get; }

        /// <summary>
        /// Is this computer infected
        /// </summary>
        bool IsInfected { get; }

        /// <summary>
        /// Sets this computer infected
        /// </summary>
        void SetInfected();

        /// <summary>
        /// Probability of making this computer infected 
        /// </summary>
        double ProbabilityOfInfection { get; }
    }
}