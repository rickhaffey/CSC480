#include <stdio.h>
#include "main.h"

int getMove_Naive(struct player* p)
{
	int c;
	int score;
	int maxColumn = 0;
	int maxScore = 0;

	// calculate the column scores
	for(c = 0; c < p->g.columns; c++)
	{  
		displayBoard(&(p->g));

		if(isMoveValid(&(p->g), c))
		{
			score = c;

			if(score > maxScore)
			{
				maxColumn = c;
				maxScore = score;
			}
		}
	}

	// return the column with the highest score
	return maxColumn;
}
