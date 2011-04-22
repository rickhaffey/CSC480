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
            Node2 solution = UniformCostSearch.Search(p);

            PrintSolution(solution);

            Console.ReadLine();
        }

        static void PrintSolution(Node2 node)
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
