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
            Game g = new Game()
            {
                Rows = 10,
                Columns = 10,
                PiecesToWin = 10,
                TimeLimitSeconds = 15
            };

            g.Initialize();

            Player p1 = new Player("Player 1");
            p1.SetGameInfo(g.Rows, g.Columns, g.PiecesToWin, 0, g.TimeLimitSeconds);
            Player p2 = new Player("Player 2");
            p2.SetGameInfo(g.Rows, g.Columns, g.PiecesToWin, 1, g.TimeLimitSeconds);
            
            GameResult result = GameResult.InProgress;
            while (result == GameResult.InProgress)
            {
                Console.WriteLine("...player 1's move...");
                int move = p1.GetNextMove();
                result = g.AcceptMove(1, move);
                p2.NoteOpponentsMove(move);

                if (result == GameResult.Win1) break;
                g.DisplayBoard();

                System.Threading.Thread.Sleep(10);

                Console.WriteLine("...player 2's move...");
                move = p2.GetNextMove();
                result = g.AcceptMove(2, move);
                p1.NoteOpponentsMove(move);

                g.DisplayBoard();
                System.Threading.Thread.Sleep(10);
            }

            Console.WriteLine(string.Format(" --- {0} --- ", result));
            g.DisplayBoard();
            Console.ReadLine();
        }
    }
}
