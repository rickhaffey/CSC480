using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.FinalProject.Connect4
{
    public class Utility
    {
        public int PlayerValue { get; set; }
        public int OpponentValue { get; set; }
        public int NormalizedValue
        {
            get
            {
                return PlayerValue - OpponentValue;
            }
        }
    }

    enum CellStates
    {
        Empty,
        Black,
        Red
    }

    public class GameValueCalculator
    {
        private Game _game;

        public GameValueCalculator(Game game)
        {
            _game = game;
        }

        internal Utility CalculateValues(Players player)
        {
            CellStates playerState = (player == Players.Black) ? CellStates.Black : CellStates.Red;
            CellStates opponentState = (player == Players.Black) ? CellStates.Red : CellStates.Black;

            Utility result = new Utility();
            result.PlayerValue = CalculatePlayerValues(playerState, opponentState);
            result.OpponentValue = CalculatePlayerValues(opponentState, playerState);

            return result;
        }

        private int CalculatePlayerValues(CellStates player, CellStates opponent)
        {
            int result = 0;

            for (int r = 0; r < _game.Rows; r++)
            {
                for (int c = 0; c < _game.Rows; c++)
                {
                    CellStates value = CellStates.Empty;
                    if(_game.Board[r, c] == Players.Black)
                        value = CellStates.Black;
                    else if(_game.Board[r, c] == Players.Red)
                        value = CellStates.Red;

                    result += CalculateCellValue(r, c, value, player, opponent);
                }
            }

            return result;
        }

        private int CalculateCellValue(int row, int column, CellStates cellState, CellStates player, CellStates opponent)
        {
            // if the cell is owned by the opponent, it has zero value
            if (cellState == opponent)
                return 0;

            int result = 0;
            result += CalculateEastWestValue(row, column, player, opponent);
            result += CalculateSouthEastValue(row, column, player, opponent);
            result += CalculateNorthSouthValue(row, column, player, opponent);
            result += CalculateSouthWestValue(row, column, player, opponent);

            return result;
        }

        private int CalculateSouthWestValue(int row, int column, CellStates player, CellStates opponent)
        {
            return 0;
        }

        private int CalculateNorthSouthValue(int row, int column, CellStates player, CellStates opponent)
        {
            int grandTotal = 0;
            int ownedPieces = 0;
            int lostPieces = 0;
            bool blocked = false;

            for (int r = Math.Max(row - (_game.PiecesToWin - 1), 0); r <= Math.Min(_game.Rows - _game.PiecesToWin, row); r++)
            {
                blocked = false;
                ownedPieces = 0;
                lostPieces = 0;

                for (int offset = 0; offset < _game.PiecesToWin; offset++)
                {

                    CellStates cellState = CellStates.Empty;
                    switch (_game.Board[r + offset, column])
                    {
                        case Players.Black:
                            cellState = CellStates.Black;
                            break;
                        case Players.Red:
                            cellState = CellStates.Red;
                            break;
                    }

                    if (cellState == opponent)
                    {
                        // this iteration should add 0 value
                        lostPieces++;
                        blocked = true;
                    }
                    else if (cellState == player)
                    {
                        ownedPieces++;
                    }
                    else
                    {
                        // do nothing if this is a blank cell
                    }
                }

                if (!blocked)
                {
                    if (ownedPieces >= _game.PiecesToWin)
                        grandTotal = 20000;
                    else
                        grandTotal += 2 ^ ownedPieces;
                }
                else if (lostPieces == (_game.PiecesToWin - 1) && ownedPieces == 1)
                {
                    // if this is a true block, make its value fall midway between a win, and 1 away from a win
                    grandTotal += (2 ^ (_game.PiecesToWin - 1) + (2 ^ _game.PiecesToWin - 2));
                }

            }

            return grandTotal;

        }

        private int CalculateSouthEastValue(int row, int column, CellStates player, CellStates opponent)
        {
            int originOffset = Math.Min(row, column);
            int endOffset = Math.Min(_game.Columns - 1 - column, _game.Rows - 1 - row);

            if ((originOffset + endOffset + 1) < _game.PiecesToWin) return 0;

            int ownedPieces = 0;
            int lostPieces = 0;
            bool blocked = false;

            int r  = row - originOffset;
            for(int c = column - originOffset; c <= (column + endOffset);)
            {
                CellStates cellState = CellStates.Empty;
                switch (_game.Board[r++, c++])
                {
                    case Players.Black:
                        cellState = CellStates.Black;
                        break;
                    case Players.Red:
                        cellState = CellStates.Red;
                        break;
                }

                if (cellState == opponent)
                {
                    // this iteration should add 0 value
                    lostPieces++;
                    blocked = true;
                }
                else if (cellState == player)
                {
                    ownedPieces++;
                }
                else
                {
                    // do nothing if this is a blank cell
                }

            }

            if (!blocked)
            {
                return 2 ^ ownedPieces;
            }
            else if (lostPieces == (_game.PiecesToWin - 1) && ownedPieces == 1)
            {
                // if this is a true block, make its value fall midway between a win, and 1 away from a win
                return (2 ^ (_game.PiecesToWin - 1) + (2 ^ _game.PiecesToWin - 2));
            }

            return 0;
        }

        private int CalculateEastWestValue(int row, int column, CellStates player, CellStates opponent)
        {
            int grandTotal = 0;
            int ownedPieces = 0;
            int lostPieces = 0;
            bool blocked = false;

            for (int c = Math.Max(column - (_game.PiecesToWin - 1), 0); c <= Math.Min(_game.Columns - _game.PiecesToWin, column); c++)
            {
                blocked = false;
                ownedPieces = 0;
                lostPieces = 0;

                for (int offset = 0; offset < _game.PiecesToWin; offset++)
                {
                    CellStates cellState = CellStates.Empty;
                    switch (_game.Board[row, c + offset])
                    {
                        case Players.Black:
                            cellState = CellStates.Black;
                            break;
                        case Players.Red:
                            cellState = CellStates.Red;
                            break;
                    }

                    if (cellState == opponent)
                    {
                        // this iteration should add 0 value
                        lostPieces++;
                        blocked = true;
                    }
                    else if (cellState == player)
                    {
                        ownedPieces++;
                    }
                    else
                    {
                        // do nothing if this is a blank cell
                    }
                }

                if (!blocked)
                {
                    if (ownedPieces >= _game.PiecesToWin)
                        grandTotal = 20000;
                    else
                        grandTotal += 2 ^ ownedPieces;
                }
                else if (lostPieces == (_game.PiecesToWin - 1) && ownedPieces == 1)
                {
                    // if this is a true block, make its value fall midway between a win, and 1 away from a win
                    grandTotal += (2 ^ (_game.PiecesToWin - 1) + (2 ^ _game.PiecesToWin - 2));
                }

            }

            return grandTotal;
        }

        public GameResult EvaluateGameState()
        {
            int blackCount, redCount;

            // check horizontal
            for (int r = 0; r < _game.Rows; r++)
            {
                // reset counters for each row
                blackCount = 0;
                redCount = 0;

                for (int c = 0; c < _game.Columns; c++)
                {
                    switch (_game.Board[r, c])
                    {
                        case Players.Black:
                            if (++blackCount >= _game.PiecesToWin) return GameResult.WinBlack;
                            redCount = 0;
                            break;
                        case Players.Red:
                            if (++redCount >= _game.PiecesToWin) return GameResult.WinRed;
                            blackCount = 0;
                            break;
                        default:
                            blackCount = redCount = 0;
                            break;
                    }
                }
            }

            // check vertical
            for (int c = 0; c < _game.Columns; c++)
            {
                // reset counters for each column
                blackCount = 0;
                redCount = 0;

                for (int r = 0; r < _game.Rows; r++)
                {
                    switch (_game.Board[r, c])
                    {
                        case Players.Black:
                            if (++blackCount >= _game.PiecesToWin) return GameResult.WinBlack;
                            redCount = 0;
                            break;
                        case Players.Red:
                            if (++redCount >= _game.PiecesToWin) return GameResult.WinRed;
                            blackCount = 0;
                            break;
                        default:
                            blackCount = redCount = 0;
                            break;
                    }
                }
            }


            int row, column;

            // check SE diagonal (starting from TOP row)
            for (int c = 0; c <= (_game.Columns - _game.PiecesToWin); c++)
            {
                // reset the counters
                blackCount = 0;
                redCount = 0;

                row = 0;
                column = c;

                while (row < _game.Rows && column < _game.Columns)
                {
                    switch (_game.Board[row, column])
                    {
                        case Players.Black:
                            if (++blackCount >= _game.PiecesToWin) return GameResult.WinBlack;
                            redCount = 0;
                            break;
                        case Players.Red:
                            if (++redCount >= _game.PiecesToWin) return GameResult.WinRed;
                            blackCount = 0;
                            break;
                        default:
                            blackCount = redCount = 0;
                            break;
                    }

                    row++;
                    column++;
                }
            }

            // check SE diagonal (starting from LEFT column)
            for (int r = 1; r <= _game.Rows - _game.PiecesToWin; r++)
            {
                // reset the counters
                blackCount = 0;
                redCount = 0;

                row = r;
                column = 0;

                while (row < _game.Rows && column < _game.Columns)
                {
                    switch (_game.Board[row, column])
                    {
                        case Players.Black:
                            if (++blackCount >= _game.PiecesToWin) return GameResult.WinBlack;
                            redCount = 0;
                            break;
                        case Players.Red:
                            if (++redCount >= _game.PiecesToWin) return GameResult.WinRed;
                            blackCount = 0;
                            break;
                        default:
                            blackCount = redCount = 0;
                            break;
                    }

                    row++;
                    column++;
                }
            }

            // check NE diagonal (starting from BOTTOM row)
            for (int c = 0; c <= (_game.Columns - _game.PiecesToWin); c++)
            {
                // reset the counters
                blackCount = 0;
                redCount = 0;

                row = _game.Rows - 1;
                column = c;

                while (row >= 0 && column < _game.Columns)
                {
                    switch (_game.Board[row, column])
                    {
                        case Players.Black:
                            if (++blackCount >= _game.PiecesToWin) return GameResult.WinBlack;
                            redCount = 0;
                            break;
                        case Players.Red:
                            if (++redCount >= _game.PiecesToWin) return GameResult.WinRed;
                            blackCount = 0;
                            break;
                        default:
                            blackCount = redCount = 0;
                            break;
                    }

                    row--;
                    column++;
                }
            }

            // check NE diagonal (starting from LEFT column)
            for (int r = _game.Rows - 2; r >= _game.PiecesToWin - 1; r--)
            {
                // reset the counters
                blackCount = 0;
                redCount = 0;

                row = r;
                column = 0;

                while (row >= 0 && column < _game.Columns)
                {
                    switch (_game.Board[row, column])
                    {
                        case Players.Black:
                            if (++blackCount >= _game.PiecesToWin) return GameResult.WinBlack;
                            redCount = 0;
                            break;
                        case Players.Red:
                            if (++redCount >= _game.PiecesToWin) return GameResult.WinRed;
                            blackCount = 0;
                            break;
                        default:
                            blackCount = redCount = 0;
                            break;
                    }

                    row--;
                    column++;
                }
            }


            // if the top row is full, return a Draw
            bool fullTopRow = true;
            for (int c = 0; c < _game.Columns; c++)
            {
                if (_game.Board[0, c] == Players.None)
                {
                    fullTopRow = false;
                    break;
                }
            }
            if (fullTopRow) return GameResult.Draw;

            return GameResult.InProgress;
        }

        public bool IsGameComplete()
        {
            switch(this.EvaluateGameState())
            {
                case GameResult.Draw:
                case GameResult.WinBlack:
                case GameResult.WinRed:
                    return true;
                default: 
                    return false;
            }
        }         
    }
}
