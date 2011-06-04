#include "HeuristicCalculator.h"
#include <math.h>
#include <iostream>

#define WIN_VALUE 20000

using std::max;
using std::min;

HeuristicCalculator::HeuristicCalculator(void)
{
}


HeuristicCalculator::~HeuristicCalculator(void)
{
}

int HeuristicCalculator::Calculate(Game* game, char player, char opponent)
{
	int playerValue = CalculatePlayerValues(game, player, opponent);
	int opponentValue = CalculatePlayerValues(game, opponent, player);
            
	return playerValue - opponentValue;
}

int HeuristicCalculator::CalculatePlayerValues(Game* game, char player, char opponent)
{
	int result = 0;

	// iterate over all the cells on the board, 
	// calculating and summing each cell's value (from player's perspective)
	for(int r = 0; r < game->GetRows(); r++)
	{
		for(int c = 0; c < game->GetColumns(); c++)
		{
			result += CalculateCellValue(game, r, c, player, opponent);
		}
	}
	
	return result;
}

int HeuristicCalculator::CalculateCellValue(Game* game, int row, int column, char player, char opponent)
{
	// if the cell is owned by the opponent, it has zero value
	if((*game->GetBoard())[row][column] == opponent)
		return 0;

	int result = 0;

	// calculate the cell's value in all 4 directions
	result += CalculateNorthSouthValue(game, row, column, player, opponent);
	result += CalculateEastWestValue(game, row, column, player, opponent);
	result += CalculateSouthEastValue(game, row, column, player, opponent);
	result += CalculateSouthWestValue(game, row, column, player, opponent);

	return result;

}

int HeuristicCalculator::CalculateEastWestValue(Game* game, int row, int column, char player, char opponent)
{
	int piecesToWin = game->GetPiecesToWin();
	int rows = game->GetRows();
	int columns = game->GetColumns();
	vector<vector<char>>* board = game->GetBoard();

	int grandTotal = 0;
	int ownedPieces = 0;
	int lostPieces = 0;
	bool blocked = false;

	for (int c = max(column - (piecesToWin - 1), 0); c <= min(columns - piecesToWin, column); c++)
	{
		blocked = false;
		ownedPieces = 0;
		lostPieces = 0;

		for (int offset = 0; offset < piecesToWin; offset++)
		{
			char cellState = (*board)[row][c + offset];

			if (cellState == opponent)
			{
				// this iteration should add 0 value
				lostPieces++;
				blocked = true;
			}
			else if (cellState == player)
			{
				ownedPieces++;
			}
			else
			{
				// do nothing if this is a blank cell
			}
		}

		if (!blocked)
		{
			grandTotal += 2 ^ ownedPieces;
		}
		else if (lostPieces == (piecesToWin - 1) && ownedPieces == 1)
		{
			// if this is a true block, make its value fall midway between a win, and 1 away from a win
			grandTotal += (2 ^ (piecesToWin - 1) + (2 ^ piecesToWin - 2));
		}

	}

	return grandTotal;
}

int HeuristicCalculator::CalculateSouthEastValue(Game* game, int row, int column, char player, char opponent)
{
	int piecesToWin = game->GetPiecesToWin();
	int rows = game->GetRows();
	int columns = game->GetColumns();
	vector<vector<char>>* board = game->GetBoard();

	int originOffset = min(row, column);
	int endOffset = min(columns - 1 - column, rows - 1 - row);

	if ((originOffset + endOffset + 1) < piecesToWin) return 0;

	int ownedPieces = 0;
	int lostPieces = 0;
	bool blocked = false;

	int r  = row - originOffset;
	for(int c = column - originOffset; c <= (column + endOffset);)
	{
		char cellState = (*board)[r++][c++];

		if (cellState == opponent)
		{
			// this iteration should add 0 value
			lostPieces++;
			blocked = true;
		}
		else if (cellState == player)
		{
			ownedPieces++;
		}
		else
		{
			// do nothing if this is a blank cell
		}
	}

	if (!blocked)
	{
		return 2 ^ ownedPieces;
	}
	else if (lostPieces == (piecesToWin - 1) && ownedPieces == 1)
	{
		// if this is a true block, make its value fall midway between a win, and 1 away from a win
		return (2 ^ (piecesToWin - 1) + (2 ^ piecesToWin - 2));
	}

	return 0;
}

int HeuristicCalculator::CalculateNorthSouthValue(Game* game, int row, int column, char player, char opponent)
{
	int piecesTowin = game->GetPiecesToWin();
	int rows = game->GetRows();
	int columns = game->GetColumns();
	vector<vector<char>>* board = game->GetBoard();

	int grandTotal = 0;
	int ownedPieces = 0;
	int lostPieces = 0;
	bool blocked = false;

	for(int r = max(row - (piecesTowin - 1), 0); r <= min(rows - piecesTowin, row); r++)
	{
		blocked = false;
		ownedPieces = 0;
		lostPieces = 0;
		
		for(int offset = 0; offset < piecesTowin; offset++)
		{
			char cellState = (*board)[r + offset][column];

			if(cellState == opponent)
			{
				// this iteration should add 0 value
				lostPieces++;
				blocked = true;
			}
			else if(cellState == player)
			{
				ownedPieces++;
			}
			else
			{
				// do nothing if this is a blank cell
			}
		}

		if(!blocked)
		{
			grandTotal += 2 ^ ownedPieces;
		}
		else if(lostPieces == (piecesTowin - 1) && ownedPieces == 1)
		{
			// if this is a true block, make its value fall midway between a win, and 1 away from a win
			grandTotal += (2 ^ (piecesTowin - 1)) + (2 ^ (piecesTowin - 2));			
		}
	}

	return grandTotal;
}

int HeuristicCalculator::CalculateSouthWestValue(Game* game, int row, int column, char player, char opponent)
{
	return 0;
}
