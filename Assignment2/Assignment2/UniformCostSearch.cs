using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    public class UniformCostSearch
    {
        public static Node2 Search(Problem2 problem)
        {
            Node2 node = new Node2() {
                State = problem.InitialState, 
                Action = new Action2() { StepCost = 0, DestState = problem.InitialState },                
                Parent = null };

            Dictionary<string, Node2> frontier = new Dictionary<string, Node2>();
            Console.WriteLine("Added node {0} to frontier [initial state].", node.State);
            frontier.Add(node.State, node);
            HashSet<string> explored = new HashSet<string>();

            while (true)
            {
                if (frontier.Count == 0) throw new Exception("Empty frontier");

                node = GetLowestCostFrontierNode(frontier);
                
                Console.WriteLine("Pulled node {0} from frontier.", node.State);

                if (problem.GoalTest(node.State)) return node;

                explored.Add(node.State);

                foreach (Action2 action in problem.Actions(node.State))
                {
                    if (!explored.Contains(action.DestState) && !frontier.ContainsKey(action.DestState))
                    {
                        Node2 newNode = new Node2() { State = action.DestState, Action = action, Parent = node };
                        frontier.Add(action.DestState, newNode);
                        Console.WriteLine("Added node {0} to frontier with path cost {1}.", newNode.State, newNode.PathCost);
                    }
                    else
                    {
                        Node2 frontierNode = GetMatchingFrontierNode(frontier, action.DestState);
                        if (frontierNode != null && frontierNode.PathCost > action.StepCost)
                        {
                            frontierNode.Action = action;
                            frontierNode.Parent = node;

                            Console.WriteLine("Updated node {0} on frontier with path cost {1}.", frontierNode.State, frontierNode.PathCost);
                        }
                    }
                }
            }
        }

        private static Node2 GetMatchingFrontierNode(Dictionary<string, Node2> frontier, string state)
        {
            return frontier.Values.ToList().Find(n => n.State == state);
        }

        private static Node2 GetLowestCostFrontierNode(Dictionary<string, Node2> frontier)
        {
            Node2 result = null;

            foreach (Node2 node in frontier.Values)
            {
                if (result == null)
                {
                    result = node;
                }
                else if (node.PathCost < result.PathCost)
                {
                    result = node;
                }
            }

            frontier.Remove(result.State);
            return result;
        }
    }
}
