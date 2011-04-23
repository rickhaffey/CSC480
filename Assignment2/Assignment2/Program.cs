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

            Problem2 p = new Problem2() { InitialState="Arad", GoalState="Bucharest" };

            int maxDepth = 0;
            Node solution = null;
            while (solution == null)
            {
                solution = UniformCostSearch.Search(p, ++maxDepth);
            }

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
