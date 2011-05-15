#include <stdio.h>
#include <stdlib.h>
#include "main.h"

int getMove_Random(struct player* p)
{
	int column = -1;
	while(!isMoveValid(&(p->g), column))
	{
		column = rand() % (p->g).columns;
	}

	return column;
}
