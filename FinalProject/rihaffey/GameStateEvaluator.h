#pragma once

#include "Game.h"

#define GS_WIN_ME 1
#define GS_WIN_OPPONENT 2
#define GS_DRAW 3
#define GS_IN_PROGRESS 4

// helper class used to determine whether the game is a win (ME or OPPONENT),
// a draw, or still ongoing
class GameStateEvaluator
{
private:
    // looks for horizontal wins
	int EvaluateHorizontal(Game* game);
    // looks for vertical wins
	int EvaluateVertical(Game* game);
    // looks for wins against the SE diagonal
	int EvaluateSouthEastDiagonal(Game* game);
    // looks for wins against the SW diagonal
	int EvaluateSouthWestDiagonal(Game* game);
    // returns a flag indicating whether the game is a draw
    // NOTE: this really just checks to see if the board is full; the assumption
    // is that the other checks all came before and didn't show a win as being present
	bool IsGameADraw(Game* game);

public:
	GameStateEvaluator(void);
	~GameStateEvaluator(void);

    // top level method to return the state of the game
	int EvaluateGameState(Game* game);
};

