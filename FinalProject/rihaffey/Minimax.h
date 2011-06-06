#pragma once
#include "Game.h"
#include <time.h>
#include "GameStateEvaluator.h"
#include "HeuristicCalculator.h"

#define MAX_DEPTH 5
#define TIMEOUT_THRESHOLD 2

class Minimax
{
private:
	time_t _startTime;
	GameStateEvaluator _gameStateEvaluator;
	HeuristicCalculator _heuristicCalculator;
	int _maxDepth;
	int _timeoutThreshold; // how many seconds short of the timeout should we abandon processing

	double MIN_VALUE(Game* game, int depth);
	double MIN_VALUE(Game* game, int depth, double alpha, double beta);
	double MAX_VALUE(Game* game, int depth);
	double MAX_VALUE(Game* game, int depth, double alpha, double beta);
	vector<int>* ACTIONS(Game* game);
	Game* RESULT(Game* game, int column, char player);
	double UTILITY(Game* game);
	bool TERMINAL_TEST(Game* game, int depth);
	bool IsWithinTimeoutThreshold(int timeLimitSeconds);



public:
	Minimax(void);
	~Minimax(void);

	int MINIMAX_DECISION(Game* game, bool alphaBetaPruning, time_t startTime);

};

