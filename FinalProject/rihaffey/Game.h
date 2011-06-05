#pragma once

#include <vector>
using std::vector;

#define ME 'x'
#define OPPONENT 'o'
#define EMPTY ' '

#define TOP_MARGIN "\r\n"
#define LEFT_MARGIN "\t"
#define BOTTOM_MARGIN "\r\n"

#define WIN -1
#define LOSS -2
#define DRAW -3
#define IN_PROGRESS -4

class Game
{
private:
	int _rows;
	int _columns;
	int _piecesToWin;
	int _timeLimitSeconds;
	vector<vector<char> > _board;

	void InitializeBoard();

public:
	Game(int rows, int columns, int piecesToWin, int timeLimitSeconds);
	~Game(void);

	int GetRows();
	int GetColumns();
	int GetPiecesToWin();
	int GetTimeLimitSeconds();
	vector<vector<char> >* GetBoard();

	void AcceptMove(char player, int column);
	void DisplayBoard();
	void CopyGameBoard(Game* source);
};




