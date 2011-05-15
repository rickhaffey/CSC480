#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "main.h"

extern void displayBoard(struct game* g);
extern struct game initializeGame(int rows, int columns, int piecesToWin, int timeLimitSeconds);
extern getNextMove(struct player* p);
extern void noteOpponentsMove(struct player* p, struct player* opponent, int column);

void main()
{
	struct game g;
	struct player p1;
	struct player p2;
	enum gameResults result = IN_PROGRESS;
	int move;

	srand((int)time(NULL));

	g = initializeGame(6, 7, 4, 15);

	p1.name = "Player 1";
	p1.g = g;
	p1.id = 1;

	p2.name = "Player 2";
	p2.g = g;
	p2.id = 2;

	while(result == IN_PROGRESS)
	{
		printf("...player 1's move...\n");
		move = getNextMove(&p1);
		result = acceptMove(&g, 1, move);
		noteOpponentsMove(&p1, &p2, move);
		displayBoard(&g);		

		if(result == IN_PROGRESS)
		{
			printf("...player 2's move...\n");
			move = getNextMove(&p2);
			result = acceptMove(&g, 2, move);
			noteOpponentsMove(&p2, &p1, move);

			displayBoard(&g);		
		}
	}

	switch(result)
	{
	case WIN1: { printf("Player 1 WIN\n"); break; }
	case WIN2: { printf("Player 2 WIN\n"); break; }
	case DRAW : { printf("DRAW\n"); break; }
	default : { printf("Unexpected result: %d\n", result); break; }
	}
}
