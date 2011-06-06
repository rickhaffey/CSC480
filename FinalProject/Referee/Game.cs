using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Referee
{
    public class Game
    {
        public const int ROWS = 6;
        public const int COLUMNS = 7;
        public const int PIECES_TO_WIN = 4;
        public const int TIME_LIMIT = 5;
        public char[,] Board;

        public const char EMPTY = ' ';
        public const char ME = 'x';
        public const char OPPONENT = 'o';

        public Game()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        { 
            Board = new char[ROWS, COLUMNS];
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLUMNS; c++)
                {
                    Board[r, c] = EMPTY;
                }
            }
        }

        public bool IsMoveValid(int column)
        {
            if (column < 0 || column > COLUMNS - 1) return false;

            return (Board[0, column] == EMPTY);
        }

        public void AcceptMove(char player, int column)
        {
            if (!IsMoveValid(column)) 
                throw new InvalidMoveException();

            for (int i = ROWS - 1; i >= 0; i--)
            {
                if (Board[i, column] == EMPTY)
                {
                    Board[i, column] = player;
                    return;
                }
            }

            throw new InvalidMoveException();
        }
    }
}



