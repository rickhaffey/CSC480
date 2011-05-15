#include <stdio.h>
#include "main.h"

extern int isMoveValid(struct game* g, int column);
extern enum gameResults acceptMove(struct game* g, int player, int column);
extern int getMove_Random(struct player* p);


struct player initializePlayer(char* name, struct game g, int turn)
{
	struct player p;
	p.name = name;
	p.g = g;
	p.turn = turn;

	return p;
}

int getNextMove(struct player* p)
{
	int column = getMove_Random(p);
	/* int column = getMove_Naive(p); */

	acceptMove(&(p->g), p->id, column);

	return column;
		
}

void noteOpponentsMove(struct player* p, struct player* opponent, int column)
{
	acceptMove(&(p->g), opponent->id, column);
}
