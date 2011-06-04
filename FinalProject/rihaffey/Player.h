#pragma once
#include "Game.h"

#define PLAYER_NAME "rihaffey"


class Player
{
private:
	int _turn;
	Game* _game;

public:
	Player(void);
	~Player(void);

	void SendName();
	void ReadConfig();
	void SendMove();
	int ReadMove();
	void ReadGameResult();

	int GetTurn();
};