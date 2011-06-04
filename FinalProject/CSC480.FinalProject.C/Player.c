#include <stdio.h>
#include "main.h"

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
	int column;
	switch(p->id)
	{
	case 1: { column = getMove_Naive(p); break; }
	case 2: { column = getMove_Random(p); break; }
	default: column = 0;
	}

	acceptMove(&(p->g), p->id, column);

	return column;
		
}

void noteOpponentsMove(struct player* p, struct player* opponent, int column)
{
	acceptMove(&(p->g), opponent->id, column);
}
