using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    public class Node
    {
        public string State { get; set; }
        public Node Parent { get; set; }
        public Action Action { get; set; }

        public int Depth
        {
            get
            {
                // recursively calculate the depth of the current node
                return (Parent == null) ? 1 : 1 + Parent.Depth;
            }
        }
        public int PathCost
        {
            get
            {                
                // recursively calculate the path cost of the current node
                return (Parent == null ? 0 : Parent.PathCost) + Problem.StepCost((Parent == null ? null : Parent.State), Action);
            }
        }

        public override string ToString()
        {
            return string.Format("#<NODE f({0}) state:{1}>", PathCost, State.ToUpper());
        }
    }
}
