using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    class Program
    {
        const int ILS = 1;
        const int UCS = 2;
        const int QUIT = 3;

        static void Main(string[] args)
        {
            int method = ShowMenu();
            while(method != QUIT)
            {
                int maxCost = (method == ILS) ? 0 : int.MaxValue;
                RunSearch(maxCost);
                method = ShowMenu();
            }
        }

        private static void RunSearch(int maxCost)
        {
            Problem p = new Problem() { InitialState = "Arad", GoalState = "Bucharest" };
            Node solution = null;

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            solution = UniformCostSearch.Search(p, maxCost);
            stopwatch.Stop();

            PrintSolution(solution, stopwatch);
        }


        private static int ShowMenu()
        {
            Console.WriteLine("{0}. ITERATIVE-LENGTHENING-SEARCH", ILS);
            Console.WriteLine("{0}. (PSEUDO-)UNIFORM-COST-SEARCH", UCS);
            Console.WriteLine("{0}. Quit", QUIT);
            Console.Write("Selection: ");
            string answer = Console.ReadLine();

            int result = 0;
            if(int.TryParse(answer, out result) && result >= 1 && result <= 3)
            {
                return result;
            }

            Console.WriteLine("** Invalid response...try again...**");
            return ShowMenu();
        }

        static void PrintSolution(Node node, System.Diagnostics.Stopwatch stopwatch)
        {
            List<string> steps = new List<string>();
            while (node != null)
            {
                steps.Add(string.Format("\t{0} - {1}", node.State, node.PathCost));
                node = node.Parent;
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine(" -- SOLUTION -- ");
            steps.Reverse();
            steps.ForEach(s => Console.WriteLine(s));
            Console.WriteLine();
            Console.WriteLine("Elapsed time: {0}", stopwatch.Elapsed);
            Console.WriteLine(new string('=', 50));
            Console.WriteLine();
        }
    }
}
