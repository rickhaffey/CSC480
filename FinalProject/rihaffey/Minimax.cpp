#include "Minimax.h"
#include <limits.h>
#include <assert.h>
#include <iostream>

using namespace std;
using std::max;
using std::min;

Minimax::Minimax(void)
{
}


Minimax::~Minimax(void)
{
}


int Minimax::MINIMAX_DECISION(Game* game, bool alphaBetaPruning)
{
	int maxValue = INT_MIN;
	vector<int> colOptions;
	vector<int>* actions = ACTIONS(game);
	
	for(unsigned int column = 0; column < actions->size(); column++)
	{
		Game* state = RESULT(game, column, ME);
		int v;
		if(alphaBetaPruning)
		{
			v = MIN_VALUE(state, 1, INT_MIN, INT_MAX);
		}
		else
		{
			v = MIN_VALUE(state, 1);
		}

		delete state;

		if(v > maxValue)
		{
			maxValue = v;
			colOptions.clear();
			colOptions.push_back(column);
		} 
		else if(v == maxValue)
		{
			colOptions.push_back(column);
		}

		/*if (_stopwatch.Elapsed.Seconds > (game.TimeLimitSeconds - 1)) break;*/
        
	}

	int c = colOptions[0]; // TODO: random selection based on the number of options

	return c;
}

int Minimax::MIN_VALUE(Game* game, int depth)
{
	if (TERMINAL_TEST(game, depth))
		return UTILITY(game);

	int value = INT_MAX;

	vector<int>* actions = ACTIONS(game);
	for(unsigned int column = 0; column < actions->size(); column++)
	{
		Game* state = RESULT(game, column, OPPONENT);
		value = min(value, MAX_VALUE(state, ++depth));
		delete state;
	}

	return value;
}

int Minimax::MIN_VALUE(Game* game, int depth, int alpha, int beta)
{
	if (TERMINAL_TEST(game, depth))
		return UTILITY(game);

	int value = INT_MAX;

	vector<int>* actions = ACTIONS(game);
	for(unsigned int column = 0; column < actions->size(); column++)
	{
		Game* state = RESULT(game, column, OPPONENT);
		value = min(value, MAX_VALUE(state, ++depth, alpha, beta));
		delete state;

		if(value <= alpha)
			return value;
		beta = min(beta, value);
	}

	return value;
}

int Minimax::MAX_VALUE(Game* game, int depth)
{
	if (TERMINAL_TEST(game, depth))
		return UTILITY(game);

	int value = INT_MIN;

	vector<int>* actions = ACTIONS(game);

	for(unsigned int column = 0; column < actions->size(); column++)
	{
		Game* state = RESULT(game, column, ME);
		value = max(value, MIN_VALUE(state, ++depth));
		delete state;
	}

	return value;
}

int Minimax::MAX_VALUE(Game* game, int depth, int alpha, int beta)
{
	if (TERMINAL_TEST(game, depth))
		return UTILITY(game);

	int value = INT_MIN;

	vector<int>* actions = ACTIONS(game);

	for(unsigned int column = 0; column < actions->size(); column++)
	{
		Game* state = RESULT(game, column, ME);
		value = max(value, MIN_VALUE(state, ++depth, alpha, beta));
		delete state;

		if(value >= beta)
			return value;
		alpha = max(alpha, value);
	}

	return value;	
}

vector<int>* Minimax::ACTIONS(Game* game)
{
	vector<int>* result = new vector<int>();
	vector<vector<char>>* board = game->GetBoard();

	for(int c = 0; c < game->GetColumns(); c++)
	{
		if((*board)[0][c] == EMPTY)
			result->push_back(c);
	}

	return result;
}

Game* Minimax::RESULT(Game* game, int column, char player)
{
	Game* newState = new Game(game->GetRows(), game->GetColumns(), game->GetPiecesToWin(), game->GetTimeLimitSeconds());
	newState->CopyGameBoard(game);
	newState->AcceptMove(player, column);

	return newState;
}

int Minimax::UTILITY(Game* game)
{	
	int result = _heuristicCalculator.Calculate(game, ME, OPPONENT);
	return result;
}

bool Minimax::TERMINAL_TEST(Game* game, int depth)
{
	//if (_stopwatch.Elapsed.Seconds > (game.TimeLimitSeconds - 2)) return true;

	if (depth >= MAX_DEPTH) return true;
	
	switch(_gameStateEvaluator.EvaluateGameState(game))
	{
	case GS_WIN_ME:
	case GS_WIN_OPPONENT:
	case GS_DRAW:
		return true;
	default:
		return false;
	}
}