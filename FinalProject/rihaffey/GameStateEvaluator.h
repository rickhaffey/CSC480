#pragma once

#include "Game.h"

#define GS_WIN_ME 1
#define GS_WIN_OPPONENT 2
#define GS_DRAW 3
#define GS_IN_PROGRESS 4

class GameStateEvaluator
{
private:
	int EvaluateHorizontal(Game* game);
	int EvaluateVertical(Game* game);
	int EvaluateSouthEastDiagonal(Game* game);
	int EvaluateSouthWestDiagonal(Game* game);
	bool IsGameADraw(Game* game);


public:
	GameStateEvaluator(void);
	~GameStateEvaluator(void);

	int EvaluateGameState(Game* game);
};

