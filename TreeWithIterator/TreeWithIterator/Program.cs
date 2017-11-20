using System;

namespace TreeWithIterator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var tree = new TreeWithIterator<int>();
            tree.Add(2);
            tree.Add(3);
            tree.Add(1);
            tree.Add(4);
            tree.Add(5);
            tree.Remove(1);
            tree.Remove(2);
            foreach(var i in tree)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("===");
            Console.WriteLine(tree.Count);
        }
    }
}
