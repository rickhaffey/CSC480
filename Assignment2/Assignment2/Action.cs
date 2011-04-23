using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.Homework2
{
    public class Action
    {
        public string DestState { get; set; }
        public int StepCost { get; set; }

        public override string ToString()
        {
            return string.Format("#<NODE f({0}) state:{1}>", StepCost, DestState.ToUpper());
        }
    }

    

}
