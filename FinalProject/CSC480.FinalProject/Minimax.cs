using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSC480.FinalProject.Connect4;
using System.IO;

namespace CSC480.FinalProject.MinimaxCs
{
    public class Minimax
    {
        const int MAX_DEPTH = 5;
        private Players _max;
        private Players _min;
        private static Random _rnd = new Random((int)DateTime.Now.Ticks);
        System.Diagnostics.Stopwatch _stopwatch;
        
        public Minimax(Players max)
        {
            _max = max;
            _min = GetOpponent(max);
        }

        public int MINIMAX_DECISION(Game game)
        {
            _stopwatch = new System.Diagnostics.Stopwatch();
            _stopwatch.Start();

            int maxValue = int.MinValue;
            List<int> colOptions = new List<int>();

            List<int> actions = ACTIONS(game);
            int iterationCounter = 0;
            foreach (int column in actions)
            {
                iterationCounter++;
                int v = MIN_VALUE(RESULT(game, column, _max), 1);

                if (v > maxValue)
                {
                    maxValue = v;
                    colOptions.Clear();
                    colOptions.Add(column);
                }
                else if (v == maxValue)
                {
                    colOptions.Add(column);
                }

                if (_stopwatch.Elapsed.Seconds > (game.TimeLimitSeconds - 1)) break;
            }

            int c = colOptions[_rnd.Next(colOptions.Count)]; 
            Console.WriteLine("Column selection: {0} / Elapsed: {1} / Total Actions: {2} / Actions Evaluated: {3}", c, _stopwatch.Elapsed, actions.Count, iterationCounter);
            return c;
        }

        public int MIN_VALUE(Game game, int depth)
        {
            if (TERMINAL_TEST(game, depth))
                return UTILITY(game);

            int value = int.MaxValue;

            foreach (int column in ACTIONS(game))
            {
                value = Math.Min(value, MAX_VALUE(RESULT(game, column, _min), ++depth));
            }

            return value;
        }

        public int MAX_VALUE(Game game, int depth)
        {
            if (TERMINAL_TEST(game, depth))
                return UTILITY(game);

            int value = int.MinValue;

            foreach (int column in ACTIONS(game))
            {
                value = Math.Max(value, MIN_VALUE(RESULT(game, column, _max), ++depth));
            }

            return value;
        }

        List<int> ACTIONS(Game game)
        {
            List<int> actions = new List<int>();

            for (int c = 0; c < game.Columns; c++)
            {
                if (game.Board[0, c] == (int)Players.None)
                    actions.Add(c);
            }

            return actions;
        }

        public Game RESULT(Game game, int column, Players playerId)
        {
            Game newState = game.Clone();
            newState.AcceptMove(playerId, column);
            return newState;
        }

        int UTILITY(Game game)
        {
            GameValueCalculator calc = new GameValueCalculator(game);
            return calc.CalculateValues(_max).NormalizedValue;
        }

        private static Players GetOpponent(Players player)
        {
            return (player == Players.Black) ? Players.Red : Players.Black;
        }


        bool TERMINAL_TEST(Game game, int depth)
        {
            if (_stopwatch.Elapsed.Seconds > (game.TimeLimitSeconds - 2)) return true;

            if (depth >= MAX_DEPTH) return true;

            GameValueCalculator calc = new GameValueCalculator(game);
            if (calc.IsGameComplete()) return true;

            return false;
        }
    }
}
