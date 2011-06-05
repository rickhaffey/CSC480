#pragma once
#include "Game.h"
#include <stdio.h>
#include <fstream>
#include <iostream>

using namespace std;

class TrainingController
{
private:
	ofstream* _file;

public:
	TrainingController(Game* game);
	~TrainingController(void);

	void AddMove(int column);
	void AddGameResult(int code, char* description);	
	void Shutdown();
};

