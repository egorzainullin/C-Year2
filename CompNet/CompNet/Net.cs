using System;

namespace CompNet
{
    public class Net
    {
        public bool[] InfectedComputers => ConvertIsInfectedToBool(computers);

        private bool[] ConvertIsInfectedToBool(IMachine[] computers)
        {
            var isInfectedArray = new bool[computers.Length];
            int length = computers.Length;
            for (int i = 0; i < length; i++)
            {
                isInfectedArray[i] = computers[i].IsInfected;
            }
            return isInfectedArray;
        }

        private IMachine[] computers;

        private bool[,] graph;

        private Random internalRandom;

        public Net(IMachine[] arrayOfComputers, bool[,] graph, Random random)
        {
            computers = arrayOfComputers;
            this.graph = graph;
            internalRandom = random;
        }

        public void NewTurn()
        {
            int length = graph.GetLength(0);
            var tempState = new bool[length];
            for (int i = 0; i < length; i++)
            {
                tempState[i] = computers[i].IsInfected;
            }
            for (int i = 0; i < length; i++)
            {
                if (computers[i].IsInfected)
                {
                    continue;
                }
                for (int j = 0; j < length; j++)
                {
                    if (graph[i, j] && tempState[j])
                    {
                        if (computers[i].ProbabilityOfInfection >= internalRandom.NextDouble())
                        {
                            computers[i].SetInfected();
                        }
                    }
                }
            }
        }

        public bool IsEndOfProcess()
        {
            int length = computers.Length;
            for (int i = 0; i < length; i++)
            {
                if (!computers[i].IsInfected && computers[i].ProbabilityOfInfection > 0)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (graph[i, j] && computers[j].IsInfected)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public Net(IMachine[] arrayOfComputers, bool[,] graph)
            : this(arrayOfComputers, graph, new Random()) { }
    }
}
