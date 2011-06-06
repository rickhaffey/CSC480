#pragma once
#include "Game.h"
#define NO_FIRST_CHANCE_PLAY -1

class FirstChancePlayHandler
{
public:
	FirstChancePlayHandler(void);
	~FirstChancePlayHandler(void);

	int GetFirstChancePlay(Game* game);
};

