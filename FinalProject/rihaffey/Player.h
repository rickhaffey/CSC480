#pragma once
#include "Game.h"
#include "TrainingController.h"

#define PLAYER_NAME "rihaffey"

class Player
{
private:
	int _turn;
	Game* _game;
	TrainingController* _trainingController;

public:
	Player(void);
	~Player(void);

	void SendName();
	void ReadConfig();
	void SendMove();
	int ReadMove();
	void ReadGameResult(int code);

	int GetTurn();
};