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

// represents all the configuration info passed from the ref at the start of the game,
// as well as the board state during the game
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

    // return a flag indicating whether the move is valid;
    // (by checking to ensure that at least one open slot is
    // still available in the requested column)
	bool IsMoveValid(int column);

    // update the state of the game to reflect a play
    // by 'player' against 'column'
	void AcceptMove(char player, int column);
	void DisplayBoard();

    // copy all the state information from the board on the
    // 'source' game into the current game's board
	void CopyGameBoard(Game* source);
};




