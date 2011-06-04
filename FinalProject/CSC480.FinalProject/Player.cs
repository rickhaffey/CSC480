using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC480.FinalProject.Connect4
{
    public class Player
    {
        protected Game _game;
        private int Turn { get; set; }
        protected Random _rnd = new Random((int)DateTime.Now.Ticks);
        private Players _id;
        private Players _opponent;

        public Players Opponent { get { return _opponent; } }

        public Player(Players id, Players opponent, string name)
        {
            _id = id;
            _opponent = opponent;
            Name = name;
        }

        public Players ID { get { return _id; } }

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
            _game.AcceptMove(ID, column);
        }

        public virtual int GetNextMove()
        {
            int column = -1;
            while(!_game.IsMoveValid(column))
                column = _rnd.Next(_game.Columns);

            _game.AcceptMove(_opponent, column);

            return column;
        }
    }
}