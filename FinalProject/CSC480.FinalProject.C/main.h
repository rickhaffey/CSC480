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


enum gameResults acceptMove(struct game* g, int playerId, int column);
int getMove_Naive(struct player* p);
int getMove_Random(struct player* p);
int getNextMove(struct player* p);
int isMoveValid(struct game* g, int column);
struct game initializeGame(int rows, int columns, int piecesToWin, int timeLimitSeconds);
void displayBoard(struct game* g);
void noteOpponentsMove(struct player* p, struct player* opponent, int column);