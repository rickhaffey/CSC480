using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    public class Problem
    {
        public string InitialState { get; set; } // starting city
        public string GoalState { get; set; } // destination city

        private Dictionary<string, List<KeyValuePair<string, int>>> _actions = null;

        public Problem(string initialState, string goalState)
        {
            InitialState = initialState;
            GoalState = goalState;
            _actions = BuildActions();
        }



        private Dictionary<string, List<KeyValuePair<string, int>>> BuildActions()
        {
            Dictionary<string, List<KeyValuePair<string, int>>> actions = new Dictionary<string, List<KeyValuePair<string, int>>>();
            List<KeyValuePair<string, int>> children = null;

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Zerind", 75));
            children.Add(new KeyValuePair<string, int>("Timisoara", 118));
            children.Add(new KeyValuePair<string, int>("Sibiu", 140));
            actions.Add("Arad", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Urziceni", 85));
            children.Add(new KeyValuePair<string, int>("Giurgiu", 90));
            children.Add(new KeyValuePair<string, int>("Pitesti", 101));
            children.Add(new KeyValuePair<string, int>("Fagaras", 211));
            actions.Add("Bucharest", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Dobreta", 120));
            children.Add(new KeyValuePair<string, int>("Rimnicu", 146));
            children.Add(new KeyValuePair<string, int>("Pitesti", 138));
            actions.Add("Craiova", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Mehadia", 75));
            children.Add(new KeyValuePair<string, int>("Craiova", 120));
            actions.Add("Dobreta", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Hirsova", 86));
            actions.Add("Eforie", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Sibiu", 99));
            children.Add(new KeyValuePair<string, int>("Bucharest", 211));
            actions.Add("Fagaras", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Bucharest", 90));
            actions.Add("Giurgiu", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Urziceni", 98));
            children.Add(new KeyValuePair<string, int>("Eforie", 86));
            actions.Add("Hirsova", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Neamt", 87));
            children.Add(new KeyValuePair<string, int>("Vaslui", 92));
            actions.Add("Iasi", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Timisoara", 111));
            children.Add(new KeyValuePair<string, int>("Mehadia", 70));
            actions.Add("Lugoj", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Lugoj", 70));
            children.Add(new KeyValuePair<string, int>("Dobreta", 75));
            actions.Add("Mehadia", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Iasi", 87));
            actions.Add("Neamt", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Zerind", 71));
            children.Add(new KeyValuePair<string, int>("Sibiu", 151));
            actions.Add("Oradea", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Rimnicu", 97));
            children.Add(new KeyValuePair<string, int>("Craiova", 138));
            children.Add(new KeyValuePair<string, int>("Bucharest", 101));
            actions.Add("Pitesti", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Sibiu", 80));
            children.Add(new KeyValuePair<string, int>("Pitesti", 97));
            children.Add(new KeyValuePair<string, int>("Craiova", 146));
            actions.Add("Rimnicu", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Arad", 140));
            children.Add(new KeyValuePair<string, int>("Oradea", 151));
            children.Add(new KeyValuePair<string, int>("Fagaras", 99));
            children.Add(new KeyValuePair<string, int>("Rimnicu", 80));
            actions.Add("Sibiu", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Arad", 118));
            children.Add(new KeyValuePair<string, int>("Lugoj", 111));
            actions.Add("Timisoara", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Bucharest", 85));
            children.Add(new KeyValuePair<string, int>("Hirsova", 98));
            children.Add(new KeyValuePair<string, int>("Vaslui", 142));
            actions.Add("Urziceni", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Iasi", 92));
            children.Add(new KeyValuePair<string, int>("Urziceni", 142));
            actions.Add("Vaslui", children);

            children = new List<KeyValuePair<string, int>>();
            children.Add(new KeyValuePair<string, int>("Arad", 75));
            children.Add(new KeyValuePair<string, int>("Oradea", 71));
            actions.Add("Zerind", children);

            return actions;
        }

        public bool GoalTest(string state)
        {
            return GoalState == state;
        }

        public List<KeyValuePair<string, int>> GetActions(string state)
        {
            return _actions[state];
        }
    }
}
