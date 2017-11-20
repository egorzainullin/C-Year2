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
            var tempGraph = new bool[graph.GetLength(0), graph.GetLength(1)];
            int length = graph.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    tempGraph[i, j] = graph[i, j];
                }
            }
        }

        public Net (IMachine[] arrayOfComputers, bool[,] graph)
            : this (arrayOfComputers, graph, new Random()) { }
    }
}
