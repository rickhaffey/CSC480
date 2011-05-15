#include <stdio.h>
#include "main.h"

struct game initializeGame(int rows, int columns, int piecesToWin, int timeLimitSeconds)
{
	struct game g;
	int r, c;
	g.rows = rows;
	g.columns = columns;
	g.piecesToWin = piecesToWin;
	g.timeLimitSeconds = timeLimitSeconds;

	for(r = 0; r < rows; r++)
	{
		for(c = 0; c < columns; c++)
		{
			g.board[r][c] = 0;
		}
	}

	return g;
}

char* getCellDisplayValue(struct game* g, int row, int column)
{
	int cellValue = g->board[row][column];
	switch(cellValue)
	{
	case 0:
		return "_";
	case 1:
		return "x";
	case 2:
		return "o";
	default:
		printf("%d", cellValue);
		return "E";
	}
}


int isMoveValid(struct game* g, int column)
{
	if(column < 0 || column > g->columns - 1) return FALSE;

	return (g->board[0][column] == 0);
}


int isBoardADraw(struct game* g)
{
	int c;
	for(c = 0; c < g->columns; c++)
	{
		if(g->board[0][c] == 0) return FALSE;
	}

	return TRUE;
}


enum gameResults acceptMove(struct game* g, int player, int column)
{
	int row;

	if(!isMoveValid(g, column))
	{
		switch(player)
		{
		case 1: return INVALID_MOVE1;
		case 2: return INVALID_MOVE2;
		default: return ERROR;
		}
	}

	for(row = g->rows - 1; row >= 0; row--)
	{
		if(g->board[row][column] == 0)
		{
			g->board[row][column] = player;
			return evaluateBoard(g, player, row, column);
		}
	}

	return ERROR;
}

enum gameResults evaluateHorizontal(struct game* g, int player, int row)
{
	int count = 0;
	int c;

	// check horizontal
	for (c = 0; c < g->columns; c++)
	{
		if (g->board[row][c] == player)
		{
			++count;
			if (count >= g->piecesToWin) 
			{
				switch(player){
				case 1: return WIN1;
				case 2: return WIN2;
				default: return ERROR;
				}
			}
		}
		else
		{
			count = 0;
		}
	}

	return IN_PROGRESS;
}


enum gameResults evaluateVertical(struct game* g, int player, int column)
{
	int count = 0;
	int r;

	// check vertical
	for (r = 0; r < g->rows; r++)
	{
		if (g->board[r][column] == player)
		{
			++count;
			if (count >= g->piecesToWin)
			{
				switch(player)
				{
				case 1: return WIN1;
				case 2: return WIN2;
				default: return ERROR;
				}
			}
		}
		else
		{
			count = 0;
		}
	}

	return IN_PROGRESS;
}


enum gameResults evaluateDiagonal1(struct game* g, int player, int row, int column)
{
	int count = 0;
	int r = row;
	int c = column;

	while (r > 0 && c < (g->columns - 1))
	{
		r--;
		c++;
	}

	while (r <= (g->rows - 1) && c >= 0)
	{
		if (g->board[r][c] == player)
		{
			++count;
			if (count >= g->piecesToWin) 
			{
				switch(player)
				{
				case 1: return WIN1;
				case 2: return WIN2;
				default: return ERROR;
				}
			}
		}
		else
		{
			count = 0;
		}

		r++;
		c--;
	}

	return IN_PROGRESS;
}

enum gameResults evaluateDiagonal2(struct game* g, int player, int row, int column)
{
	int count = 0;
	int r = row;
	int c = column;

	while (r > 0 && c >= 0)
	{
		r--;
		c--;
	}

	while (r >= 0 && r <= (g->rows - 1) && c >= 0 && c <= (g->columns - 1))
	{

		if (g->board[r][c] == player)
		{
			++count;
			if (count >= g->piecesToWin) 
			{
				switch(player)
				{
				case 1: return WIN1;
				case 2: return WIN2;
				default: return ERROR;
				}
			}
		}
		else
		{
			count = 0;
		}

		r++;
		c++;
	}

	return IN_PROGRESS;
}

enum gameResults evaluateBoard(struct game* g, int player, int row, int column)
{
	// NOTE: This only evaluates possible wins associatd with the current move            
	enum gameResults winResult;

	// if the row is 0, do a quick check to see if this move has generated a draw
	if (row == 0 && isBoardADraw(g))
	{
		return DRAW;
	}

	winResult = evaluateHorizontal(g, player, row);
	if(winResult == WIN1 || winResult == WIN2) return winResult;

	winResult = evaluateVertical(g, player, column);
	if(winResult == WIN1 || winResult == WIN2) return winResult;

	winResult = evaluateDiagonal1(g, player, row, column);
	if(winResult == WIN1 || winResult == WIN2) return winResult;

	winResult = evaluateDiagonal2(g, player, row, column);
	if(winResult == WIN1 || winResult == WIN2) return winResult;

	return IN_PROGRESS;
}


void displayBoard(struct game* g)
{
	int c, r;

	printf(TOP_MARGIN);
	printf(LEFT_MARGIN);


	for(c = 0; c < g->columns; c++)
	{
		printf(" _");
	}

	printf("\n");

	for(r = 0; r < g->rows; r++)
	{
		printf(LEFT_MARGIN);

		for(c = 0; c < g->columns; c++)
		{
			printf("|%s", getCellDisplayValue(g, r, c));
		}
		
		printf("|\n");
	}

	printf(BOTTOM_MARGIN);
}



