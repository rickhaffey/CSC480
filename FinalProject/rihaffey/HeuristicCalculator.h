#pragma once
#include "Game.h"

class HeuristicCalculator
{
private:
	int CalculatePlayerValues(Game* game, char player, char opponent);
	int CalculateCellValue(Game* game, int row, int column, char player, char opponent);

	int CalculateEastWestValue(Game* game, int row, int column, char player, char opponent);
	int CalculateSouthEastValue(Game* game, int row, int column, char player, char opponent);
	int CalculateNorthSouthValue(Game* game, int row, int column, char player, char opponent);
	int CalculateSouthWestValue(Game* game, int row, int column, char player, char opponent);

public:
	HeuristicCalculator(void);
	~HeuristicCalculator(void);

	int Calculate(Game* game, char player, char opponent);
};

