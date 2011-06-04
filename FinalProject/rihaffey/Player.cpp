#include "Player.h"
#include <iostream>
#include "HeuristicCalculator.h"
#include "Minimax.h"

using namespace std;

Player::Player()
{
}

Player::~Player()
{
	delete _game;
}

void Player::SendName()
{
	cout << PLAYER_NAME << endl;
}

void Player::ReadConfig()
{
	int rows;
	int columns;
	int pieces2Win;
	int timeLimitSeconds;

	cin >> rows >> columns >> pieces2Win >> _turn >> timeLimitSeconds;

	_game = new Game(rows, columns, pieces2Win, timeLimitSeconds);
}

int Player::ReadMove()
{
	int move;

	cin >> move;

	if(move >= 0)
	{
		_game->AcceptMove(OPPONENT, move);
		_game->DisplayBoard();
		HeuristicCalculator calc;
		int value = calc.Calculate(_game, ME, OPPONENT);

		
		cerr << "\tOpponent move: " << move << endl;
		cerr << "\tBoard value: " << value << endl;
	}
	else
		cerr << "\tGame state: " << move << endl;
			
	return move;	
}

void Player::SendMove()
{
	Minimax minimax;
	int move = minimax.MINIMAX_DECISION(_game, true);

	_game->AcceptMove(ME, move);
	_game->DisplayBoard();
	cout << move << endl;
}

int Player::GetTurn()
{
	return _turn;
}

void Player::ReadGameResult()
{
	char line[15];

	cin.getline(line, 15); // TODO : why is there a stray line coming through on std in??
	cin.getline(line, 15);

	cerr << "\tGameResult: " << line << endl;
}

