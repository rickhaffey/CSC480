using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    public class UniformCostSearch
    {
        public static Node Search(Problem problem, int maxDepth)
        {
            Console.WriteLine("Starting ({0})", maxDepth);
            Node node = new Node() {
                State = problem.InitialState, 
                Action = new Action() { StepCost = 0, DestState = problem.InitialState },                
                Parent = null };

            Dictionary<string, Node> frontier = new Dictionary<string, Node>();
            frontier.Add(node.State, node);
            HashSet<string> explored = new HashSet<string>();

            while (true)
            {
                if (frontier.Count == 0) throw new Exception("Empty frontier");

                node = GetLowestCostFrontierNode(frontier, maxDepth);

                if (node == null) return node;

                Console.WriteLine(node.ToString());

                if (problem.GoalTest(node.State)) return node;

                explored.Add(node.State);

                foreach (Action action in problem.Actions(node.State))
                {
                    Console.Write(" Checking: {0}", action);

                    if (!explored.Contains(action.DestState) && !frontier.ContainsKey(action.DestState))
                    {
                        Node newNode = new Node() { State = action.DestState, Action = action, Parent = node };
                        frontier.Add(action.DestState, newNode);

                        Console.WriteLine("++ Added");
                    }
                    else
                    {
                        Console.WriteLine();
                        Node frontierNode = GetMatchingFrontierNode(frontier, action.DestState);
                        if (frontierNode != null && frontierNode.PathCost > action.StepCost)
                        {
                            frontierNode.Action = action;
                            frontierNode.Parent = node;
                        }
                    }
                }
            }
        }        

        private static Node GetMatchingFrontierNode(Dictionary<string, Node> frontier, string state)
        {
            return frontier.Values.ToList().Find(n => n.State == state);
        }

        // todo: suboptimal approach for supporting sorting frontier by minimum path-cost
        // should implement this using a true priority queue
        private static Node GetLowestCostFrontierNode(Dictionary<string, Node> frontier, int maxDepth)
        {
            Node result = null;

            foreach (Node node in frontier.Values)
            {
                if (node.Depth > maxDepth) continue;

                if (result == null)
                {
                    result = node;
                }
                else if (node.PathCost < result.PathCost)
                {
                    result = node;
                }
            }

            if(result != null)
                frontier.Remove(result.State);

            return result;
        }
    }
}