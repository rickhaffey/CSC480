using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Referee
{
    public class RandomPlayer
    {
        private static Random _rnd = new Random((int)DateTime.Now.Ticks);

        public int GetMove(Game game)
        {
            List<int> options = new List<int>();
            for (int c = 0; c < Game.COLUMNS; c++)
            {
                if (game.IsMoveValid(c))
                    options.Add(c);
            }

            return options[_rnd.Next(options.Count)];    
        }
    }
}
