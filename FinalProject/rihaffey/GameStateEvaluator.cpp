#include "GameStateEvaluator.h"


GameStateEvaluator::GameStateEvaluator(void)
{
}


GameStateEvaluator::~GameStateEvaluator(void)
{
}


int GameStateEvaluator::EvaluateGameState(Game* game)
{
	int result = EvaluateHorizontal(game);

	if(result == GS_IN_PROGRESS)
		result = EvaluateVertical(game);

	if(result == GS_IN_PROGRESS)	
		result = EvaluateSouthEastDiagonal(game);

	if(result == GS_IN_PROGRESS)	
		result = EvaluateSouthWestDiagonal(game);

	if(result == GS_IN_PROGRESS && IsGameADraw(game))
		result = GS_DRAW;
	
	return result;
}

int GameStateEvaluator::EvaluateHorizontal(Game* game)
{
	int myCount;
	int opponentCount;

    // iterate over the rows, counting pieces by state
	for (int r = 0; r < game->GetRows(); r++)
	{
		// reset counters for each row
		myCount = 0;
		opponentCount = 0;

        // iterate over the columns
		for (int c = 0; c < game->GetColumns(); c++)
		{
            // count pieces until an opposing player's piece or blank is encountered,
            // then reset the appropriate counter(s), and start a new count;
            // if the required number for a win is reached -- return a win
			switch ((*game->GetBoard())[r][c])
			{
			case ME:
				if (++myCount >= game->GetPiecesToWin()) return GS_WIN_ME;
				opponentCount = 0;
				break;
			case OPPONENT:
				if (++opponentCount >= game->GetPiecesToWin()) return GS_WIN_OPPONENT;
				myCount = 0;
				break;
			default:
				myCount = opponentCount = 0;
				break;
			}
		}
	}

	return GS_IN_PROGRESS;
}

int GameStateEvaluator::EvaluateVertical(Game* game)
{
	int myCount = 0;
	int opponentCount = 0;

	for (int c = 0; c < game->GetColumns(); c++)
	{
		// reset counters for each column
		myCount = 0;
		opponentCount = 0;

		for (int r = 0; r < game->GetRows(); r++)
		{
            // count pieces until an opposing player's piece or blank is encountered,
            // then reset the appropriate counter(s), and start a new count;
            // if the required number for a win is reached -- return a win
			switch ((*game->GetBoard())[r][c])
			{
			case ME:
				if (++myCount >= game->GetPiecesToWin()) return GS_WIN_ME; 
				opponentCount = 0;
				break;
			case OPPONENT:
				if (++opponentCount >= game->GetPiecesToWin()) return GS_WIN_OPPONENT;
				myCount = 0;
				break;
			default:
				myCount = opponentCount = 0;
				break;
			}
		}
	}

	return GS_IN_PROGRESS;
}

int GameStateEvaluator::EvaluateSouthEastDiagonal(Game* game)
{
	int myCount = 0;
	int opponentCount = 0;

	int row, column;

	// check SE diagonal (starting from TOP row)
	for (int c = 0; c <= (game->GetColumns() - game->GetPiecesToWin()); c++)
	{
		// reset the counters
		myCount = 0;
		opponentCount = 0;

		row = 0;
		column = c;

		while (row < game->GetRows() && column < game->GetColumns())
		{
            // count pieces until an opposing player's piece or blank is encountered,
            // then reset the appropriate counter(s), and start a new count;
            // if the required number for a win is reached -- return a win
			switch ((*game->GetBoard())[row][column])
			{
			case ME:
				if (++myCount >= game->GetPiecesToWin()) return GS_WIN_ME; 
				opponentCount = 0;
				break;
			case OPPONENT:
				if (++opponentCount >= game->GetPiecesToWin()) return GS_WIN_OPPONENT; 
				myCount = 0;
				break;
			default:
				myCount = opponentCount = 0;
				break;
			}

			row++;
			column++;
		}
	}

	// check SE diagonal (starting from LEFT column)
	for (int r = 1; r <= game->GetRows() - game->GetPiecesToWin(); r++)
	{
		// reset the counters
		myCount = 0;
		opponentCount = 0;

		row = r;
		column = 0;

		while (row < game->GetRows() && column < game->GetColumns())
		{
            // count pieces until an opposing player's piece or blank is encountered,
            // then reset the appropriate counter(s), and start a new count;
            // if the required number for a win is reached -- return a win
			switch ((*game->GetBoard())[row][column])
			{
			case ME:
				if (++myCount >= game->GetPiecesToWin()) return GS_WIN_ME;
				opponentCount = 0;
				break;
			case OPPONENT:
				if (++opponentCount >= game->GetPiecesToWin()) return GS_WIN_OPPONENT;
				myCount = 0;
				break;
			default:
				myCount = opponentCount = 0;
				break;
			}

			row++;
			column++;
		}
	}

	return GS_IN_PROGRESS;
}

int GameStateEvaluator::EvaluateSouthWestDiagonal(Game* game)
{
	int myCount = 0;
	int opponentCount = 0;

	int row = 0;
	int column = 0;
	// check NE diagonal (starting from BOTTOM row)
	for (int c = 0; c <= (game->GetColumns() - game->GetPiecesToWin()); c++)
	{
		// reset the counters
		myCount = 0;
		opponentCount = 0;

		row = game->GetRows() - 1;
		column = c;

		while (row >= 0 && column < game->GetColumns())
		{
            // count pieces until an opposing player's piece or blank is encountered,
            // then reset the appropriate counter(s), and start a new count;
            // if the required number for a win is reached -- return a win
			switch ((*game->GetBoard())[row][column])
			{
			case ME:
				if (++myCount >= game->GetPiecesToWin()) return GS_WIN_ME; 
				opponentCount = 0;
				break;
			case OPPONENT:
				if (++opponentCount >= game->GetPiecesToWin()) return GS_WIN_OPPONENT;
				myCount = 0;
				break;
			default:
				myCount = opponentCount = 0;
				break;
			}

			row--;
			column++;
		}
	}

	// check NE diagonal (starting from LEFT column)
	for (int r = game->GetRows() - 2; r >= game->GetPiecesToWin() - 1; r--)
	{
		// reset the counters
		myCount = 0;
		opponentCount = 0;

		row = r;
		column = 0;

		while (row >= 0 && column < game->GetColumns())
		{
            // count pieces until an opposing player's piece or blank is encountered,
            // then reset the appropriate counter(s), and start a new count;
            // if the required number for a win is reached -- return a win
			switch ((*game->GetBoard())[row][column])
			{
			case ME:
				if (++myCount >= game->GetPiecesToWin()) return GS_WIN_ME;
				opponentCount = 0;
				break;
			case OPPONENT:
				if (++opponentCount >= game->GetPiecesToWin()) return GS_WIN_OPPONENT;
				myCount = 0;
				break;
			default:
				myCount = opponentCount = 0;
				break;
			}

			row--;
			column++;
		}
	}

	return GS_IN_PROGRESS;
}

bool GameStateEvaluator::IsGameADraw(Game* game)
{
	// if the top row is full, return a Draw
	bool fullTopRow = true;

	for (int c = 0; c < game->GetColumns(); c++)
	{
		if ((*game->GetBoard())[0][c] == EMPTY)
		{
			fullTopRow = false;
			break;
		}
	}
	if (fullTopRow) return true;

	return false;
}
