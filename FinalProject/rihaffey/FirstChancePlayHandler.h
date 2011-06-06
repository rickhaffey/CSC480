#pragma once
#include "Game.h"
#define NO_FIRST_CHANCE_PLAY -1

// a helper class responsible for checking whether there
// is either i) an immediate win possible, or ii) an immediate
// block of the opponent to avoid a loss.
// In both cases, the minimax process can be bypassed, reducing
// the processing time
class FirstChancePlayHandler
{
public:
	FirstChancePlayHandler(void);
	~FirstChancePlayHandler(void);

    // returns an integer of the column that should be played to
    // either win or block the opponent; if neither is required,
    // the NO_FIRST_CHANCE_PLAY value is returned, indicating that 
    // the minimax process should be used to determine the next move
	int GetFirstChancePlay(Game* game);
};

