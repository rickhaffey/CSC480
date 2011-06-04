#pragma once
#include "Game.h"
#include <time.h>
#include "GameStateEvaluator.h"
#include "HeuristicCalculator.h"

#define MAX_DEPTH 5

class Minimax
{
private:
	time_t _startTime;
	GameStateEvaluator _gameStateEvaluator;
	HeuristicCalculator _heuristicCalculator;

	int MIN_VALUE(Game* game, int depth);
	int MIN_VALUE(Game* game, int depth, int alpha, int beta);
	int MAX_VALUE(Game* game, int depth);
	int MAX_VALUE(Game* game, int depth, int alpha, int beta);
	vector<int>* ACTIONS(Game* game);
	Game* RESULT(Game* game, int column, char player);
	int UTILITY(Game* game);
	bool TERMINAL_TEST(Game* game, int depth);



public:
	Minimax(void);
	~Minimax(void);

	int MINIMAX_DECISION(Game* game, bool alphaBetaPruning);

};

