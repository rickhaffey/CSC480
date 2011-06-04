using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.FinalProject.Connect4
{
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException(GameResult gameResult) : base() 
        {
            GameResult = gameResult;
        }

        private GameResult _gameResult = GameResult.InvalidMoveBlack;
        public GameResult GameResult 
        {
            get
            {
                return _gameResult;
            }
            set
            {
                switch (value)
                { 
                    case Connect4.GameResult.InvalidMoveBlack:
                    case Connect4.GameResult.InvalidMoveRed:
                        _gameResult = value;
                        break;
                    default:
                        throw new Exception("Invalid game result value");
                }
            }
        }
    }
}
