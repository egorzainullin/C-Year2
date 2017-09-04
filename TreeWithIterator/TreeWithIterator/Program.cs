using System;

namespace TreeWithIterator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var tree = new TreeWithIterator<int>();
            tree.Add(1);
            tree.Add(2);
            tree.Print();
        }
    }
}
