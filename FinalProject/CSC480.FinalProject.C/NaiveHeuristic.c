#include <stdio.h>
#include "main.h"

#define WIN 100
#define FULL_COLUMN -1
#define CANT_COMPLETE -1


int getMove_Naive(struct player* p)
{
	int c;
	int score;
	int maxColumn = 0;
	int maxScore = 0;

	// calculate the column scores
	for(c = 0; c < p->g.columns; c++)
	{  
		printf("checking column %d\n", c);
		if(isMoveValid(&(p->g), c))
		{
			score = getVerticalScore(&(p->g), p->id, c);

			printf("col %d | score %d\n", c, score);

			if(score > maxScore)
			{
				maxColumn = c;
				maxScore = score;
				printf("new max: [%d(%d)]\n", maxColumn, maxScore);
			}
		}
	}

	// return the column with the highest score
	return maxColumn;
}

int getVerticalScore(struct game* g, int playerId, int column)
{
	int row = 0;
	int openSpots = 0;
	int filledSpots = 0;

	if(g->board[row][column] != 0) 
	{
		return FULL_COLUMN;
	}


	printf(">> checking column %d for player %d\n", column, playerId);

	// first, figure out how many open spots are in the column
	for(row = 0; row < g->rows; row++)
	{
		if(g->board[row][column] == 0)
			++openSpots;
		else
		{
			printf("\t[%d][%d] = %d\n", row, column, g->board[row][column]);
			break;
		}

		printf("\t[%d][%d] - openSpots: %d | filledSpots: %d\n", row, column, openSpots, filledSpots);
	}
	
	// count the number of our markers below the current spot
	for(;row < g->rows; row++)
	{
		if(g->board[row][column] == playerId)
			++filledSpots;
		else
			break;

		printf("\t[%d][%d] - *opponent's* openSpots: %d | filledSpots: %d\n", row, column, openSpots, filledSpots);
	}

	// if this is a winning position, return WIN
	if((filledSpots + 1) == g->piecesToWin) return WIN;

	// otherwise, check to see that we have enough space above to make a win
	if((openSpots + filledSpots) < g->piecesToWin)
	{
		printf("can't complete: (open:%d) | (filled:%d)\n", openSpots, filledSpots);
		return CANT_COMPLETE;
	}

	return 10 * filledSpots + openSpots;
}
