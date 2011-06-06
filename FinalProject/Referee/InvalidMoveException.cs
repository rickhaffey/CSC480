using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Referee
{
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException() : base() { }
    }
}
