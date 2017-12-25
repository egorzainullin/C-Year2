using System.Collections.Generic;

namespace Robots
{
    /// <summary>
    /// This class merges lists
    /// </summary>
    public class ListMerging
    {
        /// <summary>
        /// Merges two give lists
        /// </summary>
        /// <param name="firstList">First list to merge</param>
        /// <param name="secondList">Second list to merge</param>
        /// <returns>Merged list</returns>
        public static List<int> MergeWithoutRepeating(List<int> firstList, List<int> secondList)
        {
            var resultList = new List<int>();
            var list1 = new List<int>();
            var list2 = new List<int>();
            foreach (var item in firstList)
            {
                list1.Add(item);
            }
            foreach (var item in secondList)
            {
                list2.Add(item);
            }
            list1.Sort();
            list2.Sort();
            list1.Reverse();
            list2.Reverse();
            while (list1.Count != 0 || list2.Count != 0)
            {
                List<int> temp;
                if (list1.Count == 0)
                {
                    temp = list2;
                }
                else if (list2.Count == 0 || (list1[list1.Count - 1] < list2[list2.Count - 1]))
                {
                    temp = list1;
                }
                else if (list1[list1.Count - 1] == list2[list2.Count - 1])
                {
                    temp = list1;
                    list2.RemoveAt(list2.Count - 1);
                }
                else
                {
                    temp = list2;
                }
                int value = temp[temp.Count - 1];
                temp.RemoveAt(temp.Count - 1);
                resultList.Add(value);
            }
            return resultList;
        }
    }
}
