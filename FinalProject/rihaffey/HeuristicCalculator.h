#pragma once
#include "Game.h"

// class responsible for calculating a heuristic value associated
// with an intermediate game state

// the approach for calculating this basically involves:
// - iterating through every cell of the game board, and for each cell
// -- examine every possible win it could be a part of:
// -- = vertical
// -- = horizontal
// -- = SE diagonal
// -- = SW diagonal
// --- at each orientation, every cell could be involved in up to {piecesToWin} possible wins
// --- (ignoring cutoffs due to proximity to board edges); these values are summed to produce the
// --- cell's values
// -- the cell values are summed to produce the overall game state value

// the value of every possible win = (2 ^ the number of owned pieces), so that the value
// increases exponentially as each possible win gets closer to an actual win

// if a state represents an actual win, the calculator returns a reference value
// (much higher than a normal calculated value) to represent this

// the process also takes into consideration possible blocks for a 'save', and returns
// a value that's less than a win, but much higher than most other calculated values
// (so as to _not_ be chosen over a potential immediate win, but to be chosen in most 
// other cases over just 'strategic' options.)

// the calculator performs this from the perspective of both the player and the opponent, and 
// returns the difference between the two as the value of the given state
class HeuristicCalculator
{
private:
	double _winValue;
	double _blockValue;

	void InitializeReferenceValues(Game* game);

	double CalculatePlayerValues(Game* game, char player, char opponent);
	double CalculateCellValue(Game* game, int row, int column, char player, char opponent);

	double CalculateEastWestValue(Game* game, int row, int column, char player, char opponent);
	double CalculateSouthEastValue(Game* game, int row, int column, char player, char opponent);
	double CalculateNorthSouthValue(Game* game, int row, int column, char player, char opponent);
	double CalculateSouthWestValue(Game* game, int row, int column, char player, char opponent);

public:
	HeuristicCalculator(void);
	~HeuristicCalculator(void);

	double Calculate(Game* game, char player, char opponent);
};

