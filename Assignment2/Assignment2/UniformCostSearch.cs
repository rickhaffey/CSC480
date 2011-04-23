using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    public class UniformCostSearch
    {
        public static Node Search(Problem problem, int maxCost)
        {
            // NOTE: a couple of performance concerns:
            // - opted for simplicity over performance in the priority queue implementation -- needs review
            // - PathCost is calculated recursively (multiple times) -- this could get expensive with a deep tree

            Console.WriteLine("Starting iterative lengthening search (max cost: {0})", maxCost);
            int nextRunPathCost = 0;

            // create the root node as starting point
            Node node = new Node() 
            {
                State = problem.InitialState, 
                Action = new Action() 
                { 
                    StepCost = 0, 
                    DestState = problem.InitialState 
                },                
                Parent = null 
            };

            // creat the frontier and the explored set
            Dictionary<string, Node> frontier = new Dictionary<string, Node>();
            frontier.Add(node.State, node);
            HashSet<string> explored = new HashSet<string>();

            while (true)
            {                
                node = GetLowestCostFrontierNode(frontier);
                if (node == null)
                {
                    // if we have 'deeper' nodes (from a cost standpoint) run the search recursively, otherwise return null (no solution)
                    return (nextRunPathCost > maxCost) ? Search(problem, nextRunPathCost) : null;
                }

                Console.Write("Checking: {0}", node);
                if (node.PathCost > maxCost)
                {
                    if (nextRunPathCost == 0) nextRunPathCost = node.PathCost;
                    Console.WriteLine("--Exceeds Cost");                    
                }
                else
                {
                    // if this node is the solution, return it
                    if (problem.GoalTest(node.State)) return node;

                    Console.WriteLine("++Added");
                    explored.Add(node.State);

                    // run throught the node's children, adding them to the frontier (or updating existing paths in the frontier if there is a cost improvement)
                    foreach (Action action in problem.Actions(node.State))
                    {
                        if (!explored.Contains(action.DestState) && !frontier.ContainsKey(action.DestState))
                        {
                            // new frontier node
                            Node newNode = new Node() { State = action.DestState, Action = action, Parent = node };                            
                            frontier.Add(action.DestState, newNode);
                            Console.WriteLine("-- new frontier node: {0}", newNode);
                        }
                        else
                        {
                            Node frontierNode = GetMatchingFrontierNode(frontier, action.DestState);
                            if (frontierNode != null && frontierNode.PathCost > (node.PathCost + action.StepCost))
                            {
                                int initialCost = frontierNode.PathCost;
                                
                                // update the frontier node to reflect the improved path
                                frontierNode.Action = action;
                                frontierNode.Parent = node;
                                Console.WriteLine("-- updated frontier node with better path: {0}", frontierNode);
                                int newCost = frontierNode.PathCost;
                                Console.WriteLine("-- (Cost improvement: {0} - {1} = {2})", initialCost, newCost, initialCost - newCost);
                            }
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
        private static Node GetLowestCostFrontierNode(Dictionary<string, Node> frontier)
        {
            //Node result = frontier.FirstOrDefault(n => n.Value.PathCost == frontier.Min(n => n.Value.PathCost));

            Node result = null;

            foreach (Node node in frontier.Values)
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

            if(result != null)
                frontier.Remove(result.State);

            return result;
        }
    }
}