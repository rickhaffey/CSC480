using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    public class Node
    {
        public string State { get; set; } // represents city
        public Node Parent { get; set; }
        public string Action { get; set; } // T?
        public int PathCost { get; set; }

        

        public override bool Equals(object obj)
        {
            Node other = obj as Node;
            if (other == null) return false;

            return this.State == other.State;
        }

        public override int GetHashCode()
        {
            return State.GetHashCode() & PathCost.GetHashCode();
        }
    }
}
