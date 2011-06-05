#pragma once
#include "Game.h"

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

