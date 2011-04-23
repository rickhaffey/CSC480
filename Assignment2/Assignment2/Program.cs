using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            Problem p = new Problem() { InitialState="Arad", GoalState="Bucharest" };
            Node solution = UniformCostSearch.Search(p, 0);
            PrintSolution(solution);

            Console.ReadLine();
        }

        static void PrintSolution(Node node)
        {
            Console.WriteLine(" -- solution -- ");
            while (node != null)
            {
                Console.WriteLine("\t" + node.State);
                node = node.Parent;
            }
        }
    }
}
