using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Referee
{
    public enum GameStates { InProgress, WinMe, WinOpponent, Draw };

    public class GameEvaluator
    {
        public GameStates EvaluateGame(Game game)
        {
            GameStates result = EvaluateHorizontal(game);

            if (result == GameStates.InProgress)
                result = EvaluateVertical(game);

            if (result == GameStates.InProgress)
                result = EvaluateSouthEastDiagonal(game);

            if (result == GameStates.InProgress)
                result = EvaluateSouthWestDiagonal(game);

            if (result == GameStates.InProgress && IsGameADraw(game))
                result = GameStates.Draw;

            return result;
            
        }

        private bool IsGameADraw(Game game)
        {
            // if the top row is full, return a Draw
            bool fullTopRow = true;

            for (int c = 0; c < Game.COLUMNS; c++)
            {
                if (game.Board[0, c] == Game.EMPTY)
                {
                    fullTopRow = false;
                    break;
                }
            }
            if (fullTopRow) return true;

            return false;
        }

        public GameStates EvaluateHorizontal(Game game)
        {
            int myCount;
            int opponentCount;

            // iterate over the rows, counting pieces by state
            for (int r = 0; r < Game.ROWS; r++)
            {
                // reset counters for each row
                myCount = 0;
                opponentCount = 0;

                // iterate over the columns
                for (int c = 0; c < Game.COLUMNS; c++)
                {
                    // count pieces until an opposing player's piece or blank is encountered,
                    // then reset the appropriate counter(s), and start a new count;
                    // if the required number for a win is reached -- return a win
                    switch (game.Board[r, c])
                    {
                        case Game.ME:
                            if (++myCount >= Game.PIECES_TO_WIN) return GameStates.WinMe;
                            opponentCount = 0;
                            break;
                        case Game.OPPONENT:
                            if (++opponentCount >= Game.PIECES_TO_WIN) return GameStates.WinOpponent;
                            myCount = 0;
                            break;
                        default:
                            myCount = opponentCount = 0;
                            break;
                    }
                }
            }

            return GameStates.InProgress;
        }

        public GameStates EvaluateVertical(Game game)
        {
            int myCount = 0;
            int opponentCount = 0;

            for (int c = 0; c < Game.COLUMNS; c++)
            {
                // reset counters for each column
                myCount = 0;
                opponentCount = 0;

                for (int r = 0; r < Game.ROWS; r++)
                {
                    // count pieces until an opposing player's piece or blank is encountered,
                    // then reset the appropriate counter(s), and start a new count;
                    // if the required number for a win is reached -- return a win
                    switch (game.Board[r, c])
                    {
                        case Game.ME:
                            if (++myCount >= Game.PIECES_TO_WIN) return GameStates.WinMe;
                            opponentCount = 0;
                            break;
                        case Game.OPPONENT:
                            if (++opponentCount >= Game.PIECES_TO_WIN) return GameStates.WinOpponent;
                            myCount = 0;
                            break;
                        default:
                            myCount = opponentCount = 0;
                            break;
                    }
                }
            }

            return GameStates.InProgress;

        }

        private GameStates EvaluateSouthEastDiagonal(Game game)
        {
            int myCount = 0;
            int opponentCount = 0;

            int row, column;

            // check SE diagonal (starting from TOP row)
            for (int c = 0; c <= (Game.COLUMNS - Game.PIECES_TO_WIN); c++)
            {
                // reset the counters
                myCount = 0;
                opponentCount = 0;

                row = 0;
                column = c;

                while (row < Game.ROWS && column < Game.COLUMNS)
                {
                    // count pieces until an opposing player's piece or blank is encountered,
                    // then reset the appropriate counter(s), and start a new count;
                    // if the required number for a win is reached -- return a win
                    switch (game.Board[row, column])
                    {
                        case Game.ME:
                            if (++myCount >= Game.PIECES_TO_WIN) return GameStates.WinMe;
                            opponentCount = 0;
                            break;
                        case Game.OPPONENT:
                            if (++opponentCount >= Game.PIECES_TO_WIN) return GameStates.WinOpponent;
                            myCount = 0;
                            break;
                        default:
                            myCount = opponentCount = 0;
                            break;
                    }

                    row++;
                    column++;
                }
            }

            // check SE diagonal (starting from LEFT column)
            for (int r = 1; r <= Game.ROWS - Game.PIECES_TO_WIN; r++)
            {
                // reset the counters
                myCount = 0;
                opponentCount = 0;

                row = r;
                column = 0;

                while (row < Game.ROWS && column < Game.COLUMNS)
                {
                    // count pieces until an opposing player's piece or blank is encountered,
                    // then reset the appropriate counter(s), and start a new count;
                    // if the required number for a win is reached -- return a win
                    switch (game.Board[row, column])
                    {
                        case Game.ME:
                            if (++myCount >= Game.PIECES_TO_WIN) return GameStates.WinMe;
                            opponentCount = 0;
                            break;
                        case Game.OPPONENT:
                            if (++opponentCount >= Game.PIECES_TO_WIN) return GameStates.WinOpponent;
                            myCount = 0;
                            break;
                        default:
                            myCount = opponentCount = 0;
                            break;
                    }

                    row++;
                    column++;
                }
            }

            return GameStates.InProgress;
        }

        private GameStates EvaluateSouthWestDiagonal(Game game)
        {
            int myCount = 0;
            int opponentCount = 0;

            int row = 0;
            int column = 0;
            // check NE diagonal (starting from BOTTOM row)
            for (int c = 0; c <= (Game.COLUMNS - Game.PIECES_TO_WIN); c++)
            {
                // reset the counters
                myCount = 0;
                opponentCount = 0;

                row = Game.ROWS - 1;
                column = c;

                while (row >= 0 && column < Game.COLUMNS)
                {
                    // count pieces until an opposing player's piece or blank is encountered,
                    // then reset the appropriate counter(s), and start a new count;
                    // if the required number for a win is reached -- return a win
                    switch (game.Board[row, column])
                    {
                        case Game.ME:
                            if (++myCount >= Game.PIECES_TO_WIN) return GameStates.WinMe;
                            opponentCount = 0;
                            break;
                        case Game.OPPONENT:
                            if (++opponentCount >= Game.PIECES_TO_WIN) return GameStates.WinOpponent;
                            myCount = 0;
                            break;
                        default:
                            myCount = opponentCount = 0;
                            break;
                    }

                    row--;
                    column++;
                }
            }

            // check NE diagonal (starting from LEFT column)
            for (int r = Game.ROWS - 2; r >= Game.PIECES_TO_WIN - 1; r--)
            {
                // reset the counters
                myCount = 0;
                opponentCount = 0;

                row = r;
                column = 0;

                while (row >= 0 && column < Game.COLUMNS)
                {
                    // count pieces until an opposing player's piece or blank is encountered,
                    // then reset the appropriate counter(s), and start a new count;
                    // if the required number for a win is reached -- return a win
                    switch (game.Board[row, column])
                    {
                        case Game.ME:
                            if (++myCount >= Game.PIECES_TO_WIN) return GameStates.WinMe;
                            opponentCount = 0;
                            break;
                        case Game.OPPONENT:
                            if (++opponentCount >= Game.PIECES_TO_WIN) return GameStates.WinOpponent;
                            myCount = 0;
                            break;
                        default:
                            myCount = opponentCount = 0;
                            break;
                    }

                    row--;
                    column++;
                }
            }

            return GameStates.InProgress;
        }


    }
}


/*

bool GameStateEvaluator::IsGameADraw(Game* game)
{
}

 
 */