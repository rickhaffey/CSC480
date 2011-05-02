using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.FinalProject.Connect4
{
    public enum GameResult
    {
        InProgress,
        Draw,
        Win1,
        Win2,
        InvalidMove1,
        InvalidMove2,
        Timeout1,
        Timeout2
    }
}
