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
			Game* g = new Game(rows, columns, piecesToWin, timeLimitSeconds);
			g->CopyGameBoard(game);
			g->AcceptMove(ME, c);

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
			Game* g = new Game(rows, columns, piecesToWin, timeLimitSeconds);
			g->CopyGameBoard(game);
			g->AcceptMove(OPPONENT, c);

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
