#include "Minimax.h"
#include <limits.h>
#include <assert.h>
#include <iostream>
#include <stdlib.h>
#include <float.h>

using namespace std;

Minimax::Minimax(void)
{
	_timeoutThreshold = TIMEOUT_THRESHOLD;
}

Minimax::~Minimax(void)
{
}

int Minimax::MINIMAX_DECISION(Game* game, bool alphaBetaPruning, time_t startTime)
{
	_startTime = startTime;

	vector<int> result;
	double maxValue;
	vector<int>* actions;
	_maxDepth = 1;
	int timeLimitSeconds = game->GetTimeLimitSeconds();

	bool timeout = false;

    // loop at increasing depth levels until we near timeout
	while(!timeout)
	{
		time_t iterationTimer = time(NULL);
#if DIAGNOSTICS
		cerr << "Running Minimax at max depth " << _maxDepth << endl;
#endif
		vector<int> colOptions;
		maxValue = DBL_MIN;
		actions = ACTIONS(game);

        // check all the available actions for the 'value'
		for(unsigned int i = 0; i < actions->size(); i++)
		{
			int column = (*actions)[i];
			Game* state = RESULT(game, column, ME);
			double v;

            // select the child method to call based on
            // whether or not the caller requested alpha-beta pruning
			if(alphaBetaPruning)
			{
				v = MIN_VALUE(state, 1, DBL_MIN, DBL_MAX);
			}
			else
			{
				v = MIN_VALUE(state, 1);
			}
		
			delete state;

            // if we have a new max value, clear out our column collection,
            // and add the current column
			if(v > maxValue)
			{
				maxValue = v;
				colOptions.clear();
				colOptions.push_back(column);
			} 
            // if we have _another_ column with the same 'max' value, add it 
            // to our column collection
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
		// in the event we timeout during the next depth iteration
		if(!timeout)
		{
			result = colOptions;
#if DIAGNOSTICS
			cerr << "\tIteration complete.  Elapsed time: " << time(NULL) - iterationTimer << " Total: " << time(NULL) - _startTime << endl;
#endif
		}
		else
		{
			// if we timed out on our first depth iteration, make sure we have at least _something_ to work with
			if(colOptions.size() > 0 && result.size() == 0)
				result = colOptions;

#if DIAGNOSTICS
			cerr << "\tIteration timed out.  Elapsed time: " << time(NULL) - iterationTimer << " Total: " << time(NULL) - _startTime << endl;
#endif
		}

        // if we're close to timing out, break the loop
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

    // return the max column; if there are more than 1, randomly select one of the columns
	int selectionIndex = rand() % result.size();
	return result[selectionIndex];
}

bool Minimax::IsWithinTimeoutThreshold(int timeLimitSeconds)
{
	time_t elapsedSeconds = time(NULL) - _startTime;
	return (timeLimitSeconds - elapsedSeconds) <= _timeoutThreshold;
}

// mimics the pseudocode from AIMA MIN_VALUE
double Minimax::MIN_VALUE(Game* game, int depth)
{
	if (TERMINAL_TEST(game, depth))
		return UTILITY(game);

	double value = DBL_MAX;

	vector<int>* actions = ACTIONS(game);
	for(unsigned int i = 0; i < actions->size(); i++)
	{
		int column = (*actions)[i];
		Game* state = RESULT(game, column, OPPONENT);
		value = min(value, MAX_VALUE(state, ++depth));
		delete state;
	}

	return value;
}

// mimics the pseudocode from AIMA MIN_VALUE w/ alpha-beta pruning
double Minimax::MIN_VALUE(Game* game, int depth, double alpha, double beta)
{
	if (TERMINAL_TEST(game, depth))
		return UTILITY(game);

	double value = DBL_MAX;

	vector<int>* actions = ACTIONS(game);
	for(unsigned int i = 0; i < actions->size(); i++)
	{
		int column = (*actions)[i];
		Game* state = RESULT(game, column, OPPONENT);
		value = min(value, MAX_VALUE(state, ++depth, alpha, beta));
		delete state;

		if(value <= alpha)
			return value;
		beta = min(beta, value);
	}

	return value;
}

// mimics the pseudocode from AIMA MAX_VALUE
double Minimax::MAX_VALUE(Game* game, int depth)
{
	if (TERMINAL_TEST(game, depth))
		return UTILITY(game);

	double value = DBL_MIN;

	vector<int>* actions = ACTIONS(game);

	for(unsigned int i = 0; i < actions->size(); i++)
	{
		int column = (*actions)[i];
		Game* state = RESULT(game, column, ME);
		value = max(value, MIN_VALUE(state, ++depth));
		delete state;
	}

	return value;
}

// mimics the pseudocode from AIMA MAX_VALUE w/ alpha-beta pruning
double Minimax::MAX_VALUE(Game* game, int depth, double alpha, double beta)
{
	if (TERMINAL_TEST(game, depth))
		return UTILITY(game);

	double value = DBL_MIN;

	vector<int>* actions = ACTIONS(game);

	for(unsigned int i = 0; i < actions->size(); i++)
	{
		int column = (*actions)[i];
		Game* state = RESULT(game, column, ME);
		value = max(value, MIN_VALUE(state, ++depth, alpha, beta));
		delete state;

		if(value >= beta)
			return value;
		alpha = max(alpha, value);
	}

	return value;	
}

// mimics the pseudocode from AIMA ACTIONS; return a collection of available columns
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

// mimics the pseudocode from AIMA RESULT;
// returns the game state resulting after applying a given action
Game* Minimax::RESULT(Game* game, int column, char player)
{
	Game* newState = new Game(game->GetRows(), game->GetColumns(), game->GetPiecesToWin(), game->GetTimeLimitSeconds());
	newState->CopyGameBoard(game);
	newState->AcceptMove(player, column);

	return newState;
}

// mimics the pseudocode from AIMA UTILITY;
// uses the heuristic calculator to generate the utility value
double Minimax::UTILITY(Game* game)
{	
	double result = _heuristicCalculator.Calculate(game, ME, OPPONENT);
	return result;
}

// mimics the pseudocode from AIMA TERMINAL_TEST;
// uses the game state evaluator to perform the determination;
// also checks timeout conditions and iteration depth as part 'exit' criteria
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

double max(double lhs, double rhs)
{
	return (lhs > rhs) ? lhs : rhs;	
}

double min(double lhs, double rhs)
{
	return (lhs < rhs) ? lhs : rhs;	
}
