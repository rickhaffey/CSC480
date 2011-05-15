#define MAXROWS 10
#define MAXCOLS 10
#define TRUE 1
#define FALSE 0

#define TOP_MARGIN "\n"
#define LEFT_MARGIN "\t"
#define BOTTOM_MARGIN "\n"


enum gameResults { IN_PROGRESS, DRAW, WIN1, WIN2, INVALID_MOVE1, INVALID_MOVE2, TIMEOUT1, TIMEOUT2, ERROR };


struct game {
	int rows;
	int columns;
	int piecesToWin;
	int timeLimitSeconds;
	int board[MAXROWS][MAXCOLS];
};


struct player {
	char* name;
	struct game g;
	int turn;
	int id;
};
