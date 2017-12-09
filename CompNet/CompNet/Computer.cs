namespace CompNet
{
    public class Computer : IMachine
    {
        /// <summary>
        /// Type of OS
        /// </summary>
        public string TypeOfOS { get; private set; }

        /// <summary>
        /// Is this computer infected
        /// </summary>
        public bool IsInfected { get; private set; }

        /// <summary>
        /// Possibility to make this computer infected
        /// </summary>
        public double ProbabilityOfInfection { get; private set; }

        /// <summary>
        /// Sets this computer infected
        /// </summary>
        public void SetInfected()
        {
            IsInfected = true;
        }

        /// <summary>
        /// Creates an instance of <see cref="Computer"/>
        /// </summary>
        /// <param name="typeOfOS">Type of OS</param>
        /// <param name="probability">Possibility of infection</param>
        /// <param name="isInfected">Is this computer infected</param>
        public Computer(string typeOfOS, double probability, bool isInfected)
        {
            TypeOfOS = typeOfOS;
            if (probability >= 1)
            {
                ProbabilityOfInfection = 1;
            }
            else if (probability <= 0)
            {
                ProbabilityOfInfection = 0;
            }
            else
            {
                ProbabilityOfInfection = probability;
            }
            IsInfected = isInfected; 
        }
    }
}
