using System;

namespace CompNet
{
    /// <summary>
    /// This class is responsible for virus spreading emulation 
    /// </summary>
    public class Net
    {
        /// <summary>
        /// Array which show which computers are infected
        /// </summary>
        public bool[] InfectedComputers => ConvertIsInfectedToBool(computers);

        /// <summary>
        /// Converts comp's array into boolean array
        /// </summary>
        /// <param name="computers">Array to convert</param>
        /// <returns>Converted array</returns>
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

        /// <summary>
        /// Array of computers
        /// </summary>
        private IMachine[] computers;

        /// <summary>
        /// Graph of dependencies
        /// </summary>
        private bool[,] graph;

        /// <summary>
        /// Random to generate possibilities
        /// </summary>
        private Random internalRandom;

        /// <summary>
        /// Initializes a new instance of the <see cref="Net" />
        /// NOTE: we consider graph directional and we don't care about vertex which enters itself
        /// </summary>
        /// <param name="arrayOfComputers">Set of computers</param>
        /// <param name="graph">Graph of dependencies</param>
        /// <param name="random">Random to generate infections' possibilities</param>
        public Net(IMachine[] arrayOfComputers, bool[,] graph, Random random)
        {
            computers = arrayOfComputers;
            this.graph = graph;
            internalRandom = random;
        }

        /// <summary>
        /// Emulates a new turn and infects some computers
        /// </summary>
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
                    if (graph[j, i] && tempState[j])
                    {
                        if (computers[i].ProbabilityOfInfection >= internalRandom.NextDouble())
                        {
                            computers[i].SetInfected();
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks is the process 100% ends 
        /// </summary>
        /// <returns>Is end of process</returns>
        public bool IsEndOfProcess()
        {
            int length = computers.Length;
            for (int i = 0; i < length; i++)
            {
                if (!computers[i].IsInfected && computers[i].ProbabilityOfInfection > 0)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (graph[j, i] && computers[j].IsInfected)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Net" />
        /// NOTE: we consider graph directional and we don't care about vertex which enters itself
        /// </summary>
        /// <param name="arrayOfComputers">Set of computers</param>
        /// <param name="graph">Graph of dependencies</param>
        public Net(IMachine[] arrayOfComputers, bool[,] graph)
            : this(arrayOfComputers, graph, new Random()) { }
    }
}
