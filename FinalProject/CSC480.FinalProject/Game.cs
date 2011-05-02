using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.FinalProject.Connect4
{
    public class Game
    {
        private int[,] _board;

        public int Rows { get; set; }
        public int Columns { get; set; }
        public int PiecesToWin { get; set; }
        public int TimeLimitSeconds { get; set; }

        public int[,] Board { get { return _board; } }

        public void Initialize()
        {
            _board = new int[Rows, Columns];
        }

        public void DisplayBoard()
        {
            const string TOP_MARGIN = "\r\n";
            const string LEFT_MARGIN = "\t";
            const string BOTTOM_MARGIN = "\r\n";

            Console.Write(TOP_MARGIN);

            // output the top border
            Console.Write(LEFT_MARGIN);
            for (int c = 0; c < Columns; c++)
            {
                Console.Write(" _");
            }
            Console.WriteLine("");

            for (int r = 0; r < Rows; r++)
            {
                Console.Write(LEFT_MARGIN);

                for (int c = 0; c < Columns; c++)
                {
                    Console.Write(string.Format("|{0}", GetCellDisplayValue(r, c)));
                }
                Console.WriteLine("|");
            }

            Console.Write(BOTTOM_MARGIN);
        }

        private string GetCellDisplayValue(int row, int column)
        {

            switch (_board[row, column])
            {
                case 0:
                    return "_";
                case 1:
                    return "x";
                case 2:
                    return "o";
                default:
                    throw new Exception("Unexcpected cell value.");
            }
        }

        public bool IsMoveValid(int column)
        {
            if (column < 0 || column > Columns - 1) return false;

            return (_board[0, column] == 0);
        }

        public GameResult AcceptMove(int player, int column)
        {
            if (!IsMoveValid(column))
            {
                switch (player)
                {
                    case 1:
                        return GameResult.InvalidMove1;
                    case 2:
                        return GameResult.InvalidMove2;
                    default:
                        throw new Exception("Unexpected player value");
                }
            }

            for (int i = Rows - 1; i >= 0; i--)
            {
                if (_board[i, column] == 0)
                {
                    _board[i, column] = player;
                    return EvaluateBoard(player, i, column);                    
                }
            }

            throw new Exception("Unable to process player move.");
        }

        private bool IsBoardADraw()
        {
            // if we find any columns with 0 in the first row, it's not yet a draw
            for (int c = 0; c < Columns; c++)
            {
                if (_board[0, c] == 0) return false;
            }

            return true;
        }

        private GameResult EvaluateBoard(int player, int row, int column)
        {
            // NOTE: This only evaluates possible wins associatd with the current move            
            GameResult winResult = (player == 1) ? GameResult.Win1 : GameResult.Win2;
            int count;

            // if the row is 0, do a quick check to see if this move has generated a draw
            if (row == 0 && IsBoardADraw())
            {
                return GameResult.Draw;
            }

            // check horizontal
            count = 0;
            for (int c = 0; c < Columns; c++)
            {
                if (_board[row, c] == player)
                {
                    ++count;
                    if (count >= PiecesToWin) return winResult;
                }
                else
                {
                    count = 0;
                }
            }

            // check vertical
            count = 0;
            for (int r = 0; r < Rows; r++)
            {
                if (_board[r, column] == player)
                {
                    ++count;
                    if (count >= PiecesToWin) return winResult;
                }
                else
                {
                    count = 0;
                }
            }

            // check diagonal 1
            count = 0;
            int r0 = row; int c0 = column;
            while (r0 > 0 && c0 < (Columns - 1))
            {
                r0--;
                c0++;
            }

            while (r0 <= (Rows - 1) && c0 >= 0)
            {
                if (_board[r0, c0] == player)
                {
                    ++count;
                    if (count >= PiecesToWin) return winResult;
                }
                else
                {
                    count = 0;
                }

                r0++;
                c0--;
            }

            // check diagonal 2
            count = 0;
            r0 = row; c0 = column;
            while (r0 > 0 && c0 >= 0)
            {
                r0--;
                c0--;
            }

            while (r0 >= 0 && r0 <= (Rows - 1) && c0 >= 0 && c0 <= (Columns - 1))
            {

                if (_board[r0, c0] == player)
                {
                    ++count;
                    if (count >= PiecesToWin) return winResult;
                }
                else
                {
                    count = 0;
                }

                r0++;
                c0++;
            }

            return GameResult.InProgress;
        }
    }
}
