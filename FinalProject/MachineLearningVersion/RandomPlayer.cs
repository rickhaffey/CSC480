using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Referee;

namespace MachineLearningVersion
{
    public class RandomPlayer
    {
        const string TRAINING_FILES_DIR = @"training_data";
        private const string TRAINING_FILE_NAME_FORMAT = "c4run_{0}.training";
        private string _currentSequence;
        private Game _game;
        private Random _rnd;
        private StreamWriter _writer;

        public RandomPlayer()
        {
            _currentSequence = string.Empty;
            _rnd = new Random((int)DateTime.Now.Ticks);

            _writer = new StreamWriter(Path.Combine(GetTrainingFileDirectoryPath(), string.Format(TRAINING_FILE_NAME_FORMAT, DateTime.Now.Ticks)));
        }

        private string GetTrainingFileDirectoryPath()
        {
            string workingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string trainingDir = Path.Combine(workingPath, TRAINING_FILES_DIR);

            return trainingDir;
        }

        public void SendName()
        {
            Console.WriteLine("rihaffey.Random");
        }

        public void ReadConfig()
        {
            string config = Console.ReadLine();

            _game = new Game();
            // NOTE: training currently set up to only work with standard (6 7 4 ...) connect 4 config -- 
            // file format would have to change (rather than concat directly, columns would need to be delimited, etc.)
        }

        public int GetTurn()
        {
            return 0;
        }

        public void SendMove()
        {
            int move = GetMove();

            _game.AcceptMove('x', move);
            _currentSequence += move.ToString();

            _writer.Write(move.ToString());

            Console.WriteLine(move.ToString());
        }

        public int ReadMove()
        {
            string line = Console.ReadLine();
            int move = int.Parse(line.Trim());

            if (move < 0) return move;

            _game.AcceptMove('o', move);
            _currentSequence += move.ToString();

            _writer.Write(move.ToString());

            return move;
        }

        public void ReadGameResult(int code)
        {
            string line = Console.ReadLine();
            _writer.WriteLine();
            _writer.WriteLine(code.ToString());
            _writer.WriteLine(line);
            _writer.Close();
        }

        public int GetMove()
        {
            return GetRandomValidColumn(_game, new List<int> { 0, 1, 2, 3, 4, 5, 6 });
        }

        private int GetRandomValidColumn(Game game, List<int> options)
        {
            int result = -1;
            bool isValid = false;
            int temp;
            while (!isValid && options.Count > 0)
            {
                temp = options[_rnd.Next(options.Count)];
                isValid = _game.IsMoveValid(temp);
                if (isValid) result = temp;
            }

            return result;
        }
    }
}
