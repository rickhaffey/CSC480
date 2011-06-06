using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Referee;
using System.IO;

namespace MachineLearningVersion
{
    public class GameDetail
    {
        public GameDetail(string trainingFilePath)
        {
            StreamReader reader = new StreamReader(trainingFilePath);

            PlaySequence = reader.ReadLine();

            if (string.IsNullOrEmpty(PlaySequence)) throw new InvalidTrainingFileException();

            string result = reader.ReadLine();
            if (result == "-1")
                Result = GameStates.WinMe;
            else if (result == "-2")
                Result = GameStates.WinOpponent;
            else if (result == "-3")
                Result = GameStates.Draw;

            reader.Close();
        }

        public string PlaySequence { get; set; }


        public GameStates Result { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int Value
        {
            get
            {
                return Wins - Losses;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(PlaySequence);
            sb.AppendFormat("Wins: {0}, Losses: {1}, Draws: {2}{3}", Wins, Losses, Draws, Environment.NewLine);

            return sb.ToString(); 
        }
    }
}
