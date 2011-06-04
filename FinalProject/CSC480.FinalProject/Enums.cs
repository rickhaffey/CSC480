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
        WinBlack,
        WinRed,
        InvalidMoveBlack,
        InvalidMoveRed,
        TimeoutBlack,
        TimeoutRed
    }

    public enum Players
    {
        None = 0,
        Black = 1,
        Red = 2
    }

}
