using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.FinalProject.Connect4
{
    public class Game
    {
        private Players[,] _board;

        public int Rows { get; set; }
        public int Columns { get; set; }
        public int PiecesToWin { get; set; }
        public int TimeLimitSeconds { get; set; }
        public Players[,] Board { get { return _board; } }

        public void Initialize()
        {
            _board = new Players[Rows, Columns];
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

        public void DisplayBoard(System.IO.StreamWriter writer)
        {
            const string TOP_MARGIN = "\r\n";
            const string LEFT_MARGIN = "\t";
            const string BOTTOM_MARGIN = "\r\n";

            writer.Write(TOP_MARGIN);

            // output the top border
            writer.Write(LEFT_MARGIN);
            for (int c = 0; c < Columns; c++)
            {
                writer.Write(" _");
            }
            writer.WriteLine("");

            for (int r = 0; r < Rows; r++)
            {
                writer.Write(LEFT_MARGIN);

                for (int c = 0; c < Columns; c++)
                {
                    writer.Write(string.Format("|{0}", GetCellDisplayValue(r, c)));
                }
                writer.WriteLine("|");
            }

            writer.Write(BOTTOM_MARGIN);               
        }

        private string GetCellDisplayValue(int row, int column)
        {

            switch (_board[row, column])
            {
                case 0:
                    return "_";
                case Players.Black:
                    return "b";
                case Players.Red:
                    return "r";
                default:
                    throw new Exception("Unexcpected cell value.");
            }
        }

        public bool IsMoveValid(int column)
        {
            if (column < 0 || column > Columns - 1) return false;

            return (_board[0, column] == 0);
        }

        public void AcceptMove(Players player, int column)
        {
            if (!IsMoveValid(column))
            {
                switch (player)
                {
                    case Players.Black:
                        throw new InvalidMoveException(GameResult.InvalidMoveBlack);
                    case Players.Red:
                        throw new InvalidMoveException(GameResult.InvalidMoveRed);
                    default:
                        throw new Exception("Unexpected player value");
                }
            }

            for (int i = Rows - 1; i >= 0; i--)
            {
                if (_board[i, column] == Players.None)
                {
                    _board[i, column] = player;
                    return;
                }
            }

            throw new Exception("Unable to process player move.");
        }

        public Game Clone()
        {
            Game clone = new Game()
            {
                Rows = this.Rows,
                Columns = this.Columns,
                PiecesToWin = this.PiecesToWin,
                TimeLimitSeconds = this.TimeLimitSeconds
            };

            clone.Initialize();

            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    clone.Board[r, c] = this.Board[r, c];
                }
            }

            return clone;
        }

    }
}
