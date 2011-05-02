using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.FinalProject.Connect4
{
    public class Player
    {
        private Game _game;
        private int Turn { get; set; }
        private Random _rnd = new Random((int)DateTime.Now.Ticks);

        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public void SetGameInfo(int rows, int columns, int piecesToWin, int turn, int timeLimitSeconds)
        {
            _game = new Game()
            {
                Rows = rows,
                Columns = columns,
                PiecesToWin = piecesToWin,
                TimeLimitSeconds = timeLimitSeconds
            };
            _game.Initialize();

            Turn = turn;
        }

        public void NoteOpponentsMove(int column)
        {
            _game.AcceptMove(2, column);
        }

        public int GetNextMove()
        {
            int column = -1;
            while(!_game.IsMoveValid(column))
                column = _rnd.Next(_game.Columns);

            _game.AcceptMove(1, column);

            return column;
        }
    }
}
