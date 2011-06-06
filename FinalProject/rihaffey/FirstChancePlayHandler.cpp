#include "FirstChancePlayHandler.h"
#include "GameStateEvaluator.h"

FirstChancePlayHandler::FirstChancePlayHandler(void)
{
}


FirstChancePlayHandler::~FirstChancePlayHandler(void)
{
}

int FirstChancePlayHandler::GetFirstChancePlay(Game* game)
{
	int rows = game->GetRows();
	int columns = game->GetColumns();
	int piecesToWin = game->GetPiecesToWin();
	int timeLimitSeconds = game->GetTimeLimitSeconds();
	
	GameStateEvaluator evaluator;

	// check for next move wins, and if present, send that
	for(int c = 0; c < columns; c++)
	{
		if(game->IsMoveValid(c))
		{
            // create a copy of the game state, and apply the move
			Game* g = new Game(rows, columns, piecesToWin, timeLimitSeconds);
			g->CopyGameBoard(game);
			g->AcceptMove(ME, c);

            // check to see if it's a win, and if so, return the column
			int gameState = evaluator.EvaluateGameState(g);
			
			delete g;

			if(gameState == GS_WIN_ME)
			{
				return c;
			}
		}
	}

	// next, check for any blocks, and if present, send that
	for(int c = 0; c < columns; c++)
	{
		if(game->IsMoveValid(c))
		{
            // create a copy of the game state, and apply the move
			Game* g = new Game(rows, columns, piecesToWin, timeLimitSeconds);
			g->CopyGameBoard(game);
			g->AcceptMove(OPPONENT, c);

            // check to see if it's a loss, and if so, return the column (as a block)
			int gameState = evaluator.EvaluateGameState(g);
			
			delete g;

			if(gameState == GS_WIN_OPPONENT)
			{
				return c;
			}
		}
	}

	// no first chance move, so indicate that.
	return NO_FIRST_CHANCE_PLAY;
}
