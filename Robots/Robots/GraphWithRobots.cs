using System.Collections.Generic;

namespace Robots
{
    /// <summary>
    /// This class emulates how robots move and checks possibility of whole destruction 
    /// </summary>
    public class GraphWithRobots
    {
        /// <summary>
        /// Given graph
        /// </summary>
        public bool[,] Graph { get; private set; }

        /// <summary>
        /// Initial robots' positions
        /// </summary>
        public IList<int> Positions { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphWithRobots"/> class
        /// </summary>
        /// <param name="graph">Given graph</param>
        /// <param name="positions">Initial robots' positions</param>
        public GraphWithRobots(bool[,] graph, IList<int> positions)
        {
            this.Graph = graph;
            this.Positions = positions;
        }

        /// <summary>
        /// Checks is it possible to destroy all robots
        /// </summary>
        /// <returns>Is it possible</returns>
        public bool IsAllDamaged()
        {
            var reachedFromZero = NodesCanBeReachedFromThis(0);
            int count1 = 0;
            foreach (var robot in Positions)
            {
                if (reachedFromZero.Contains(robot))
                {
                    ++count1;
                }
            }
            int count2 = Positions.Count - count1;
            return count1 != 1 && count2 != 1;
        }

        /// <summary>
        /// Gets neighbors of this node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<int> GetNeighbors(int node)
        {
            int length = Graph.GetLength(0);
            var listOfNodes = new List<int>();
            for (int j = 0; j < length; j++)
            {
                if (Graph[node, j] && j != node)
                {
                    listOfNodes.Add(j);
                }
            }
            listOfNodes.Sort();
            return listOfNodes;
        }

        /// <summary>
        /// Gets list of positions that can be reached by robot in start position 
        /// </summary>
        /// <param name="start">Position where robot starts</param>
        /// <returns>All possible positions where robot can move</returns>
        public IList<int> NodesCanBeReachedFromThis(int start)
        {
            int length = Graph.GetLength(0);
            bool[] visited = new bool[length];
            for (int i = 0; i < length; i++)
            {
                visited[i] = false;
            }
            List<int> GetReachableNodes(int current)
            {
                visited[current] = true;
                var resultList = new List<int>();
                var nodesNearBy = GetNeighbors(current);
                foreach (var node in nodesNearBy)
                {
                    if (!visited[node])
                    {
                        var temp = GetNeighbors(node);

                        resultList = ListMerging.MergeWithoutRepeating(resultList, temp);
                    }
                }
                if (resultList.Count == 0)
                {
                    return new List<int>() { current };
                }
                foreach (var node in resultList)
                {
                    if (!visited[node])
                    {
                        var temp = GetReachableNodes(node);
                        resultList = ListMerging.MergeWithoutRepeating(resultList, temp);
                    }
                }
                return ListMerging.MergeWithoutRepeating(resultList, new List<int> { current });
            }
            return GetReachableNodes(start);
        }
    }
}
