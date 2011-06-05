#include "Game.h"
#include <iostream>
#include <assert.h>

using namespace std;

Game::Game(int rows, int columns, int piecesToWin, int timeLimitSeconds)
{
	_rows = rows;
	_columns = columns;
	_piecesToWin = piecesToWin;
	_timeLimitSeconds = timeLimitSeconds;

	InitializeBoard();
}

Game::~Game()
{

}

int Game::GetRows()
{
	return _rows;
}


int Game::GetColumns()
{
	return _columns;
}

int Game::GetPiecesToWin()
{
	return _piecesToWin;
}
	
int Game::GetTimeLimitSeconds()
{
	return _timeLimitSeconds;
}

vector<vector<char> >* Game::GetBoard()
{
	return &(_board);
}

void Game::AcceptMove(char player, int column)
{
	assert(IsMoveValid(column));
	
	for (int i = _rows - 1; i >= 0; i--)
	{
		if (_board[i][column] == EMPTY)
		{
			_board[i][column] = player;
			return;
		}
	}

	// we should never get here -- assert to confirm that during debugging	
	assert(0);	
}

bool Game::IsMoveValid(int column)
{
	if (column < 0 || column > GetColumns() - 1) return false;

	return (_board[0][column] == EMPTY);
}

void Game::InitializeBoard()
{
	_board.resize(_rows);
	for(int r = 0; r < _rows; r++)
	{
		_board[r].resize(_columns);

		for(int c = 0; c < _columns; c++)
		{
			_board[r][c] = EMPTY;
		}
	}
}

void Game::DisplayBoard()
{
#if DIAGNOSTICS
	cerr << TOP_MARGIN;
	cerr << LEFT_MARGIN;

	for (int c = 0; c < _columns; c++)
	{
		cerr << " _";
	}

	cerr << endl;

	for (int r = 0; r < _rows; r++)
	{
		cerr << LEFT_MARGIN;

		for (int c = 0; c < _columns; c++)
		{
			cerr << "|" << _board[r][c];                    
		}
		cerr << "|" << endl;
	}

	cerr << BOTTOM_MARGIN;               
#endif
}

void Game::CopyGameBoard(Game* source)
{
	// note: this logic resizes the board if the source is different from the destination
	// -- this really shouldn't ever happen -- we should probably throw an exception in this case
	_board.clear();

	int rows = source->GetRows();
	int columns = source->GetColumns();
	vector<vector<char> >* sourceBoard = source->GetBoard();

	_board.resize(rows);
	for(int r = 0; r < rows; r++)
	{
		_board[r].resize(columns);
		for(int c = 0; c < columns; c++)
		{
			_board[r][c] = (*sourceBoard)[r][c];
		}
	}
}
