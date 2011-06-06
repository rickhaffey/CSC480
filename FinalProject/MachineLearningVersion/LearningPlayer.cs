using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Referee;

namespace MachineLearningVersion
{
    public class LearningPlayer
    {
        const string TRAINING_FILES_DIR = @"training_data";
        private const string TRAINING_FILE_NAME_FORMAT = "c4run_{0}.training";
        List<GameDetail> _knowledgeBase;
        private string _currentSequence;
        private Game _game;
        private Random _rnd;
        private StreamWriter _writer;

        public LearningPlayer()
        {
            _currentSequence = string.Empty;
            _rnd = new Random((int)DateTime.Now.Ticks);
            _knowledgeBase = new List<GameDetail>();
            LoadTrainingData();

            _writer = new StreamWriter(Path.Combine(GetTrainingFileDirectoryPath(), string.Format(TRAINING_FILE_NAME_FORMAT, DateTime.Now.Ticks)));

            foreach (GameDetail gd in _knowledgeBase)
            {
                Console.WriteLine(gd.PlaySequence);
            }
        }

        private string GetTrainingFileDirectoryPath()
        {
            string workingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string trainingDir = Path.Combine(workingPath, TRAINING_FILES_DIR);

            return trainingDir;
        }

        private void LoadTrainingData()
        {
            foreach (string file in Directory.GetFiles(GetTrainingFileDirectoryPath()))
            {
                try
                {
                    _knowledgeBase.Add(new GameDetail(file));
                    _knowledgeBase.Sort(SortGameDetails);
                }
                catch (InvalidTrainingFileException)
                {
                    Console.WriteLine("Skipping invalid training file: {0}", file);
                }
            }
        }

        private int SortGameDetails(GameDetail lhs, GameDetail rhs)
        {
            int result = lhs.PlaySequence.CompareTo(rhs.PlaySequence);
            return result;
        }

        public void SendName()
        {
            Console.WriteLine("rihaffey.LearningPlayer");
        }

        public void ReadConfig()
        {
            string config = Console.ReadLine();
           
            _game = new Game();
            // NOTE: training currently set up to only work with standard (6 7 4 ...) connect 4 config -- 
            // file format would have to change (rather than concat directly, columns would need to be delimited, etc.)
        }

        public void SendMove()
        {
            int move = GetTurn();

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

        public int GetTurn()
        {
            // look through knowledge base to find a matching entry (up to our current point)
            List<GameDetail> matches = _knowledgeBase.FindAll(gd => gd.PlaySequence.StartsWith(_currentSequence));
            matches.Sort(SortMatches);
            
            // if no match, return a random valid move
            if(matches.Count == 0)
                return GetRandomValidColumn(_game, new List<int> { 0, 1, 2, 3, 4, 5, 6 });

            // if best historical play on record has more losses than wins, play a random column out of columns not recorded in KB
            if (matches[0].Value <= 0)
            { 
                // first, collect all the columns that have been tried in this scenario
                List<int> columns = new List<int>();
                foreach (GameDetail gd in matches)
                {
                    int nextMoveColumn = GetNextMoveColumn(gd.PlaySequence, _currentSequence);
                    if (!columns.Contains(nextMoveColumn)) columns.Add(nextMoveColumn);
                }

                // now, randomly select one of the columns not in the list
                List<int> choices = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
                choices.RemoveAll(c => columns.Contains(c));

                return GetRandomValidColumn(_game, choices);
            }

            // otherwise, pick the column that has shown the best win history (highest diff between wins and losses && lowest loss count)
            return GetNextMoveColumn(matches[0].PlaySequence, _currentSequence);
        }

        private int GetNextMoveColumn(string detailSequence, string currentSequence)
        {
            int result = 0;

            string nextColString = detailSequence.Substring(currentSequence.Length, 1);
            result = int.Parse(nextColString);

            return result;
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

        private int SortMatches(GameDetail lhs, GameDetail rhs)
        {
            return lhs.Value.CompareTo(rhs.Value);
        }
    }
}
