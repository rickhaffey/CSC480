using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    public class Problem
    {
        private Dictionary<string, List<Action>> _actions;
        public string InitialState { get; set; }
        public string GoalState { get; set; }

        public Problem()
        {
            InitializeActions();
        }

        private void InitializeActions()
        {
            _actions = new Dictionary<string, List<Action>>();
            List<Action> actions = new List<Action>();
            actions.Add(new Action() { DestState = "Zerind", StepCost = 75 });
            actions.Add(new Action() { DestState = "Timisoara", StepCost = 118 });
            actions.Add(new Action() { DestState = "Sibiu", StepCost = 140 });
            _actions.Add("Arad", actions);
            
            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Urziceni", StepCost = 85 });
            actions.Add(new Action() { DestState = "Giurgiu", StepCost = 90 });
            actions.Add(new Action() { DestState = "Pitesti", StepCost = 101 });
            actions.Add(new Action() { DestState = "Fagaras", StepCost = 211 });
            _actions.Add("Bucharest", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Dobreta", StepCost = 120 });
            actions.Add(new Action() { DestState = "Rimnicu", StepCost = 146 });
            actions.Add(new Action() { DestState = "Pitesti", StepCost = 138 });
            _actions.Add("Craiova", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Mehadia", StepCost = 75 });
            actions.Add(new Action() { DestState = "Craiova", StepCost = 120 });
            _actions.Add("Dobreta", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Hirsova", StepCost = 86 });
            _actions.Add("Eforie", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Sibiu", StepCost = 99 });
            actions.Add(new Action() { DestState = "Bucharest", StepCost = 211 });
            _actions.Add("Fagaras", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Bucharest", StepCost = 90 });
            _actions.Add("Giurgiu", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Urziceni", StepCost = 98 });
            actions.Add(new Action() { DestState = "Eforie", StepCost = 86 });
            _actions.Add("Hirsova", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Neamt", StepCost = 87 });
            actions.Add(new Action() { DestState = "Vaslui", StepCost = 92 });
            _actions.Add("Iasi", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Timisoara", StepCost = 111 });
            actions.Add(new Action() { DestState = "Mehadia", StepCost = 70 });
            _actions.Add("Lugoj", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Lugoj", StepCost = 70 });
            actions.Add(new Action() { DestState = "Dobreta", StepCost = 75 });
            _actions.Add("Mehadia", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Iasi", StepCost = 87 });
            _actions.Add("Neamt", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Zerind", StepCost = 71 });
            actions.Add(new Action() { DestState = "Sibiu", StepCost = 151 });
            _actions.Add("Oradea", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Rimnicu", StepCost = 97 });
            actions.Add(new Action() { DestState = "Craiova", StepCost = 138 });
            actions.Add(new Action() { DestState = "Bucharest", StepCost = 101 });
            _actions.Add("Pitesti", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Sibiu", StepCost = 80 });
            actions.Add(new Action() { DestState = "Pitesti", StepCost = 97 });
            actions.Add(new Action() { DestState = "Craiova", StepCost = 146 });
            _actions.Add("Rimnicu", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Arad", StepCost = 140 });
            actions.Add(new Action() { DestState = "Oradea", StepCost = 151 });
            actions.Add(new Action() { DestState = "Fagaras", StepCost = 99 });
            actions.Add(new Action() { DestState = "Rimnicu", StepCost = 80 });
            _actions.Add("Sibiu", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Arad", StepCost = 118 });
            actions.Add(new Action() { DestState = "Lugoj", StepCost = 111 });
            _actions.Add("Timisoara", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Bucharest", StepCost = 85 });
            actions.Add(new Action() { DestState = "Hirsova", StepCost = 98 });
            actions.Add(new Action() { DestState = "Vaslui", StepCost = 142 });
            _actions.Add("Urziceni", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Iasi", StepCost = 92 });
            actions.Add(new Action() { DestState = "Urziceni", StepCost = 142 });
            _actions.Add("Vaslui", actions);

            actions = new List<Action>();
            actions.Add(new Action() { DestState = "Arad", StepCost = 75 });
            actions.Add(new Action() { DestState = "Oradea", StepCost = 71 });
            _actions.Add("Zerind", actions);


        }

        public bool GoalTest(string state)
        {
            return state == GoalState;
        }

        public List<Action> Actions(string state)
        {            
            return _actions[state];
        }

        public static string Result(string state, Action action)
        {
            return action.DestState;
        }

        public static int StepCost(string state, Action action)
        {
            return action.StepCost;
        }
    }
}
