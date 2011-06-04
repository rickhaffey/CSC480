using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.FinalProject.Connect4
{
    public class MinimaxPlayer : Player
    {
        private MinimaxCs.Minimax minimax;

        public MinimaxPlayer(Players id, Players opponent, string name) : base(id, opponent, name) 
        {
            minimax = new MinimaxCs.Minimax(this.ID);
        }

        public override int GetNextMove()
        {
            int nextCol = minimax.MINIMAX_DECISION(_game);
            
            System.Diagnostics.Debug.Assert(_game.IsMoveValid(nextCol));
           
            _game.AcceptMove(this.ID, nextCol);

            return nextCol;
        }
    }
}
