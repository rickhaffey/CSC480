#include <stdio.h>
#include <stdlib.h>
#include "main.h"

extern int isMoveValid(struct game* g, int column);
extern enum gameResults acceptMove(struct game* g, int player, int column);

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
	int column = -1;

	while(!isMoveValid(&(p->g), column))
	{
		column = rand() % (p->g).columns;
	}

	acceptMove(&(p->g), 1, column);

	return column;
}

void noteOpponentsMove(struct player* p, int column)
{
	acceptMove(&(p->g), 2, column);
}
