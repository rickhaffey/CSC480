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
	if(_trainingController != NULL)
	{

		delete _trainingController;
	}
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
	_trainingController = new TrainingController(_game);
}

int Player::ReadMove()
{
	int move;

	cin >> move;

	if(move >= 0)
	{
		_game->AcceptMove(OPPONENT, move);
		_game->DisplayBoard();

		_trainingController->AddMove(move);

#if DIAGNOSTICS
		cerr << "\tOpponent move: " << move << endl;
	}
	else

		cerr << "\tGame state: " << move << endl;
#else
	}
#endif
			
	return move;	
}

void Player::SendMove()
{
	Minimax minimax;
	int move = minimax.MINIMAX_DECISION(_game, true);

	_game->AcceptMove(ME, move);
	_game->DisplayBoard();
	cout << move << endl;

	_trainingController->AddMove(move);
}

int Player::GetTurn()
{
	return _turn;
}

void Player::ReadGameResult(int code)
{
	char line[15];

	cin.getline(line, 15); // TODO : why is there a stray line coming through on std in??
	cin.getline(line, 15);

	_trainingController->AddGameResult(code, line);
	_trainingController->Shutdown();

#if DIAGNOSTICS
	cerr << "\tGameResult: " << line << endl;
#endif
}

