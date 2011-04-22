using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    public class Node2
    {
        public string State { get; set; }
        public Node2 Parent { get; set; }
        public Action2 Action { get; set; }
        public int PathCost
        {
            get
            {                
                return (Parent == null ? 0 : Parent.PathCost) + Problem2.StepCost((Parent == null ? null : Parent.State), Action);
            }
        }
    }
}
