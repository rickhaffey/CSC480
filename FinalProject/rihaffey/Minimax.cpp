#include "Minimax.h"
#include <limits.h>
#include <assert.h>
#include <iostream>
#include <stdlib.h>

using namespace std;
using std::max;
using std::min;

Minimax::Minimax(void)
{
	_startTime = time(NULL);
	_timeoutThreshold = TIMEOUT_THRESHOLD;
}


Minimax::~Minimax(void)
{
}


int Minimax::MINIMAX_DECISION(Game* game, bool alphaBetaPruning)
{
	vector<int> result;
	int maxValue;
	vector<int>* actions;
	_maxDepth = 1;
	int timeLimitSeconds = game->GetTimeLimitSeconds();

	bool timeout = false;

	while(!timeout)
	{
		time_t iterationTimer = time(NULL);
		cerr << "Running Minimax at max depth " << _maxDepth << endl;
		vector<int> colOptions;
		maxValue = INT_MIN;
		actions = ACTIONS(game);

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

			// if we're within 1 second of timing out, just end this iteration
			// and use the last level's results
			if(IsWithinTimeoutThreshold(timeLimitSeconds))
			{
				timeout = true;
				break;
			}
		}

		// copy our result at each depth, so that we have the 'best' result so far
		// int the event we timeout during the next depth iteration
		if(!timeout)
		{
			result = colOptions;
			cerr << "\tIteration complete.  Elapsed time: " << time(NULL) - iterationTimer << " Total: " << time(NULL) - _startTime << endl;
		}
		else
		{
			// if we timed out on our first depth iteration, make sure we have at least _something_ to work with
			if(colOptions.size() > 0 && result.size() == 0)
				result = colOptions;

			cerr << "\tIteration timed out.  Elapsed time: " << time(NULL) - iterationTimer << " Total: " << time(NULL) - _startTime << endl;
		}

		if(IsWithinTimeoutThreshold(timeLimitSeconds))
		{
			timeout = true;
		}
		else
		{
			// run again 
			_maxDepth++;
		}
	}

	int selectionIndex = rand() % result.size();
	return result[selectionIndex];
}

bool Minimax::IsWithinTimeoutThreshold(int timeLimitSeconds)
{
	time_t elapsedSeconds = time(NULL) - _startTime;
	return (timeLimitSeconds - elapsedSeconds) <= _timeoutThreshold;
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
	vector<vector<char> >* board = game->GetBoard();

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
	if(IsWithinTimeoutThreshold(game->GetTimeLimitSeconds())) return true;	

	if (depth >=_maxDepth) return true;
	
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
