#include "HeuristicCalculator.h"
#include <math.h>
#include <iostream>

#define ORIENTATIONS 4 // NORTH-SOUTH, EAST-WEST, NORTH-EAST, SOUTH-EAST
#define BLOCK_VALUE_RATIO .75

using std::max;
using std::min;

HeuristicCalculator::HeuristicCalculator(void)
{
}


HeuristicCalculator::~HeuristicCalculator(void)
{
}

double HeuristicCalculator::Calculate(Game* game, char player, char opponent)
{
	InitializeReferenceValues(game);

    // calculate the value of the game state from each player's perspective
	double playerValue = CalculatePlayerValues(game, player, opponent);
	double opponentValue = CalculatePlayerValues(game, opponent, player);
            
    // return the difference, to indicate the 'normalized' value from player's perspective
	return playerValue - opponentValue;
}

void HeuristicCalculator::InitializeReferenceValues(Game* game)
{
	// set up our comparison values
	int piecesToWin = game->GetPiecesToWin();
	// if we see a winning game state, represent this with the max calculated value for a board
	_winValue = (double)((2 ^ piecesToWin) * (piecesToWin) * (ORIENTATIONS) * game->GetRows() * game->GetColumns());

	// treat a block value as only slightly less than a win
	_blockValue = _winValue * BLOCK_VALUE_RATIO;
}

double HeuristicCalculator::CalculatePlayerValues(Game* game, char player, char opponent)
{
	double result = 0.0;

	// iterate over all the cells on the board, 
	// calculating and summing each cell's value (from player's perspective)
	for(int r = 0; r < game->GetRows(); r++)
	{
		for(int c = 0; c < game->GetColumns(); c++)
		{
			result += CalculateCellValue(game, r, c, player, opponent);
			if(result >= _winValue) return result;
		}
	}
	
	return result;
}

double HeuristicCalculator::CalculateCellValue(Game* game, int row, int column, char player, char opponent)
{
	double result = 0.0;

	// if the cell is owned by the opponent, it has zero value
	if((*game->GetBoard())[row][column] == opponent)
		return result;

	// calculate the cell's value in all 4 directions; if we find a win
    // at any point in the calculations, short-circuit and return the value
	result += CalculateNorthSouthValue(game, row, column, player, opponent);
	if(result >= _winValue) return result;
	result += CalculateEastWestValue(game, row, column, player, opponent);
	if(result >= _winValue) return result;
	result += CalculateSouthEastValue(game, row, column, player, opponent);
	if(result >= _winValue) return result;
	result += CalculateSouthWestValue(game, row, column, player, opponent);

	return result;
}

double HeuristicCalculator::CalculateEastWestValue(Game* game, int row, int column, char player, char opponent)
{
	int piecesToWin = game->GetPiecesToWin();
	int rows = game->GetRows();
	int columns = game->GetColumns();
	vector<vector<char> >* board = game->GetBoard();

	double grandTotal = 0.0;
	int ownedPieces = 0;
	int lostPieces = 0;
	bool blocked = false;

    // look to the left and right of the cell based on the required pieces to win
	for (int c = max(column - (piecesToWin - 1), 0); c <= min(columns - piecesToWin, column); c++)
	{
        // reset the iteration values on each pass
		blocked = false;
		ownedPieces = 0;
		lostPieces = 0;

        // at each location, go through all the 'required' slots
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

        // if a potential win wasn't blocked this pass by an opponent's piece,
        // return a win if that's the case, otherwise calculate a value
        // based on how many pieces are held in this pass
		if (!blocked)
		{
			if(ownedPieces >= piecesToWin)
				return _winValue;
			else
				grandTotal += (double)(2 ^ ownedPieces);
		}
        // otherwise, if there's the potential for blocking an immediate win
        // by the opponent, indicate that
		else if (lostPieces == (piecesToWin - 1) && ownedPieces == 1)
		{
			// if this is a true block, make its value less than a win, but more than most other states
			return _blockValue;
		}
	}

	return grandTotal;
}

double HeuristicCalculator::CalculateSouthEastValue(Game* game, int row, int column, char player, char opponent)
{
    // NOTE: see 'CalculateEastWestValue' for details of the algorithm
	int piecesToWin = game->GetPiecesToWin();
	int rows = game->GetRows();
	int columns = game->GetColumns();
	vector<vector<char> >* board = game->GetBoard();

	int originOffset = min(row, column);
	int endOffset = min(columns - 1 - column, rows - 1 - row);

	if ((originOffset + endOffset + 1) < piecesToWin) return 0;

	double grandTotal = 0.0;
	int ownedPieces = 0;
	int lostPieces = 0;
	bool blocked = false;

	int r  = row - originOffset;
	for(int c = column - originOffset; c <= (column + endOffset - (piecesToWin - 1)); )
	{
		for(int offset = 0; offset < piecesToWin; offset++)
		{
			char cellState = (*board)[r + offset][c + offset];

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
			if(ownedPieces >= piecesToWin)
				return _winValue;
			else
				grandTotal += (double)(2 ^ ownedPieces);
		}
		else if (lostPieces == (piecesToWin - 1) && ownedPieces == 1)
		{
			// if this is a true block, make its value fall midway between a win, and 1 away from a win
			return _blockValue;
		}

		r++;
		c++;
	}

	return grandTotal;
}

double HeuristicCalculator::CalculateNorthSouthValue(Game* game, int row, int column, char player, char opponent)
{
    // NOTE: see 'CalculateEastWestValue' for details of the algorithm
	int piecesTowin = game->GetPiecesToWin();
	int rows = game->GetRows();
	int columns = game->GetColumns();
	vector<vector<char> >* board = game->GetBoard();

	double grandTotal = 0.0;
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
			if(ownedPieces >= piecesTowin)
				return _winValue;
			else
				grandTotal += (double)(2 ^ ownedPieces);
		}
		else if(lostPieces == (piecesTowin - 1) && ownedPieces == 1)
		{
			// if this is a true block, add our reference value to the grand total
			return _blockValue;
		}
	}

	return grandTotal;
}

double HeuristicCalculator::CalculateSouthWestValue(Game* game, int row, int column, char player, char opponent)
{
    // NOTE: see 'CalculateEastWestValue' for details of the algorithm
	int piecesToWin = game->GetPiecesToWin();
	int rows = game->GetRows();
	int columns = game->GetColumns();
	vector<vector<char> >* board = game->GetBoard();

	int startOffset = min(rows - row - 1, column);
	int endOffset = min(columns - column - 1, row);

	if ((startOffset + endOffset + 1) < piecesToWin) return 0;

	double grandTotal = 0.0;
	int ownedPieces = 0;
	int lostPieces = 0;
	bool blocked = false;

	int r  = row + startOffset;
	for(int c = column - startOffset; c <= (column + endOffset - (piecesToWin - 1)); )
	{
		for(int offset = 0; offset < piecesToWin; offset++)
		{
			char cellState = (*board)[r - offset][c + offset];

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
			if(ownedPieces >= piecesToWin)
				return _winValue;
			else
				grandTotal += (double)(2 ^ ownedPieces);
		}
		else if (lostPieces == (piecesToWin - 1) && ownedPieces == 1)
		{
			// if this is a true block, make its value fall midway between a win, and 1 away from a win
			return _blockValue;
		}

		r--;
		c++;
	}

	return grandTotal;
}
