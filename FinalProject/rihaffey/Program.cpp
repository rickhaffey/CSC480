#include "Player.h"
#include <iostream>

using namespace std;

int main(int argc, char** argv)
{
	Player p;
	
	// send player identification to the referee
	p.SendName();

	// read the config info sent from the referee
	// {#rows} {#columns} {#pieces2win} {turn [0 1]} {timeLimitSeconds}
	p.ReadConfig();

	// if our turn is '0', send the first move
	bool sendMove = (p.GetTurn() == 0);

	int gameResult = 0;
	//+int (opponent move), -1 (win), -2 (loss), -3 (tie)
	while(gameResult != WIN && gameResult != LOSS && gameResult != DRAW)
	{
		if(sendMove)
		{
			p.SendMove();	
		}

		gameResult = p.ReadMove();
		sendMove = true;
	}

	p.ReadGameResult();

#if DEBUG
	cerr << "Press <ENTER> to end...";
	char line[1];
	cin.getline(line, 1);
#endif

    return 0;
}



/*
NOTE:
- use std out for communication TO  ref
- use std in for communication FROM ref
- use std error for debug writing to console

>> 10 seconds to announce NAME on std out
<< referee will send: {#rows} {#columns} {#pieces2win} {turn [0 1]} {timeLimitSeconds}

if turn == 1
>> send move

loop
	<< read game state from ref: +int (opponent move), -1 (win), -2 (loss), -3 (tie)
		[if negative, exit loop]
	>> send move

<< read final game result from ref: DRAW, WIN1, WIN2, INVALID_MOVE1, INVALID_MOVE2, TIMEOUT1, TIMEOUT2

EXIT
*/
