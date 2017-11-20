namespace CompNet
{
    public class Computer : IMachine
    {
        public string TypeOfOs { get; private set; }

        public bool IsInfected { get; private set; }

        public double ProbabilityOfInfection { get; private set; }

        public void SetInfected()
        {
            IsInfected = true;
        }

        public Computer(string typeOfOs, double probability, bool isInfected)
        {
            TypeOfOs = typeOfOs;
            ProbabilityOfInfection = probability;
            IsInfected = isInfected; 
        }
    }
}
