﻿using System;
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
                if (Parent == null) return 1;
                return 1 + Parent.Depth;
            }
        }
        public int PathCost
        {
            get
            {                
                return (Parent == null ? 0 : Parent.PathCost) + Problem.StepCost((Parent == null ? null : Parent.State), Action);
            }
        }

        public override string ToString()
        {
            return string.Format("#<NODE f({0}) state:{1}>", PathCost, State.ToUpper());
        }
    }
}
