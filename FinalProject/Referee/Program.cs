using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Referee
{
    class Program
    {
        const string MINIMAX_EXE_FILENAME = @"C:\Work\CSC480\FinalProject\rihaffey\Debug\rihaffey.exe";
        const string ML_EXE_FILENAME = @"C:\Work\CSC480\FinalProject\MachineLearningVersion\bin\Debug\MachineLearningVersion.exe";

        static void Main(string[] args)
        {
            //for (int i = 0; i < 1000; i++)
            {
                RunGame();
            }

            DisplayMessageGreen("**** DONE ****");

            Console.ReadLine();
        }

        private static void RunGame()
        {
            Process p = StartAiPlayer();

            string line = p.StandardOutput.ReadLine();
            DisplayMessageGreen(string.Format("running player: {0}", line));
            string config = string.Format("{0} {1} {2} 0 {3}", Game.ROWS, Game.COLUMNS, Game.PIECES_TO_WIN, Game.TIME_LIMIT);
            DisplayMessageGreen(string.Format("game config: {0}", config));
            p.StandardInput.WriteLine(config);

            Game game = new Game();
            GameEvaluator evaluator = new GameEvaluator();
            RandomPlayer player2 = new RandomPlayer();
            GameStates gameState = GameStates.InProgress;

            while (gameState == GameStates.InProgress)
            {
                // read the move from player
                line = p.StandardOutput.ReadLine();
                DisplayMessageGreen(string.Format("move from player: {0}", line));

                // tie the move to the game state and evaluate
                game.AcceptMove(Game.ME, int.Parse(line.Trim()));
                gameState = evaluator.EvaluateGame(game);
                if (gameState != GameStates.InProgress)
                {
                    HandleEndGame(gameState, p);
                    break;
                }

                // get random player's move
                int move2 = player2.GetMove(game);

                game.AcceptMove(Game.OPPONENT, move2);
                gameState = evaluator.EvaluateGame(game);
                if (gameState != GameStates.InProgress)
                {
                    HandleEndGame(gameState, p);
                    break;
                }

                DisplayMessageGreen(string.Format("sending move: {0}", move2));
                p.StandardInput.WriteLine(move2);
            }

            p.WaitForExit();
        }

        private static void HandleEndGame(GameStates gameState, Process p)
        {
            switch (gameState)
            {
                case GameStates.WinMe:
                    p.StandardInput.WriteLine("-1");
                    p.StandardInput.WriteLine("WIN1");
                    DisplayMessageBlue("AI player wins");
                    break;
                case GameStates.WinOpponent:
                    p.StandardInput.WriteLine("-2");
                    p.StandardInput.WriteLine("WIN2");
                    DisplayMessageRed("AI player loses");
                    break;
                case GameStates.Draw:
                    p.StandardInput.WriteLine("-3");
                    p.StandardInput.WriteLine("DRAW");
                    DisplayMessageGreen("Draw");
                    break;
            }
        }

        static void DisplayMessageGreen(string message)
        {
            //DisplayMessage(message, ConsoleColor.DarkGreen);
        }

        static void DisplayMessageRed(string message)
        {
            DisplayMessage(message, ConsoleColor.DarkRed);
        }

        static void DisplayMessageBlue(string message)
        {
            DisplayMessage(message, ConsoleColor.DarkBlue);
        }

        static void DisplayMessage(string message, ConsoleColor color)
        {
            ConsoleColor origColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = origColor;        
        }

        static Process StartAiPlayer()
        {
            // start up the ai player
            string arguments = "";
            ProcessStartInfo si = new ProcessStartInfo(ML_EXE_FILENAME, arguments);
            //ProcessStartInfo si = new ProcessStartInfo(MINIMAX_EXE_FILENAME, arguments);
            si.UseShellExecute = false;
            si.RedirectStandardOutput = true;
            si.RedirectStandardInput = true;

            return Process.Start(si);
        }
    }
}
