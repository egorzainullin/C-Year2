using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompNet
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var matrix = new bool[,] { { true, false, true },
                                       { false, true, true },
                                       { true, true, true } };
            var computers = new IMachine[3];
            var factory = new CompFactory();
            computers[0] = factory.CreateComp(OS.Linux, true);
            computers[1] = factory.CreateComp(OS.Mac, false);
            computers[2] = factory.CreateComp(OS.Windows, false);
            var net = new Net(computers, matrix);
            for (int i = 0; i < 4; i++)
            {
                foreach (var comp in net.InfectedComputers)
                {
                    Console.Write(comp + " ");
                }
                Console.WriteLine();
                net.NewTurn();
            }
        }
    }
}
