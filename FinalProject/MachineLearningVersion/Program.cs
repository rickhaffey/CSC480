using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MachineLearningVersion
{
    class Program
    {
        const int WIN = -1;
        const int LOSS = -2;
        const int DRAW = -3;

        static void Main(string[] args)
        {
            LearningPlayer p = new LearningPlayer();

            // send player identification to the referee
            p.SendName();

            // read the config info sent from the referee
            // {#rows} {#columns} {#pieces2win} {turn [0 1]} {timeLimitSeconds}
            p.ReadConfig();

            // if our turn is '0', send the first move
            bool sendMove = (p.GetTurn() == 0);

            int gameResultCode = 0;
            //+int (opponent move), -1 (win), -2 (loss), -3 (tie)
            while (gameResultCode != WIN && gameResultCode != LOSS && gameResultCode != DRAW)
            {
                if (sendMove)
                {
                    p.SendMove();
                }

                gameResultCode = p.ReadMove();
                sendMove = true;
            }

            p.ReadGameResult(gameResultCode);
        }
    }
}
