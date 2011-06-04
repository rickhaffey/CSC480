using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSC480.FinalProject.Connect4;

namespace CSC480.FinalProject.DevDriver
{
    class Program
    {
        static void Main(string[] args)
        {

            //DiagnoseFdUpMoves();
            //return;

            System.IO.StreamWriter writer = new System.IO.StreamWriter("output.txt");
            writer.AutoFlush = true;
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(" *** Playing iteration {0} ***", i);
                PlayGame(writer);
            }

            writer.Close();




            //string response = "Y";
            //while (response.ToUpper() == "Y")
            //{
            //    PlayGame();
            //    Console.WriteLine("");
            //    Console.Write("Play again? (Y/N)");
            //    response = Console.ReadLine();
            //}
        }

        static void DiagnoseFdUpMoves()
        {
            Game g = new Game()
            {
                Rows = 6,
                Columns = 7,
                PiecesToWin = 4,
                TimeLimitSeconds = 30000
            };

            g.Initialize();

            g.AcceptMove(Players.Black, 3);
            g.AcceptMove(Players.Red, 4);
            g.AcceptMove(Players.Black, 3);
            g.AcceptMove(Players.Red, 3);

            MinimaxCs.Minimax m = new MinimaxCs.Minimax(Players.Black);

            int col = m.MINIMAX_DECISION(g);



        }

        static void PlayGame(System.IO.StreamWriter writer)
        {
            Game g = new Game()
            {
                Rows = 6,
                Columns = 7,
                PiecesToWin = 4,
                TimeLimitSeconds = 30
            };

            g.Initialize();

            Player p1 = new MinimaxPlayer(Players.Black, Players.Red, "MINIMAX (1)");
            p1.SetGameInfo(g.Rows, g.Columns, g.PiecesToWin, 0, g.TimeLimitSeconds);

            Player p2 = new Player(Players.Red, Players.Black, "SIMPLE (2)");
            p2.SetGameInfo(g.Rows, g.Columns, g.PiecesToWin, 1, g.TimeLimitSeconds);

            GameValueCalculator calc = new GameValueCalculator(g);
            
            GameResult result = GameResult.InProgress;
            while (result == GameResult.InProgress)
            {
                int move = p1.GetNextMove();
                g.AcceptMove(p1.ID, move);
                result = calc.EvaluateGameState();
                p2.NoteOpponentsMove(move);

                if (result == GameResult.WinBlack || result == GameResult.WinRed) break;
                
                move = p2.GetNextMove();
                g.AcceptMove(p2.ID, move);
                result = calc.EvaluateGameState();
                p1.NoteOpponentsMove(move);
            }

            Console.WriteLine(string.Format(" --- {0} --- ", result));
            g.DisplayBoard();

            writer.WriteLine(string.Format(" --- {0} --- ", result));
            g.DisplayBoard(writer);
        }
    }
}
