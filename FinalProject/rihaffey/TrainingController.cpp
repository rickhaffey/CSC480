#include "TrainingController.h"
#include <time.h>

TrainingController::TrainingController(Game* game)
{
#if TRAINING
  char filename[30];
  //sprintf(filename,".\\training_data\\c4run_%d.training",time(NULL));
  sprintf(filename,"c4run_%d.training",time(NULL));
  _file = new ofstream(filename, ios::out);
#endif
}

TrainingController::~TrainingController(void)
{
}

void TrainingController::AddMove(int column)
{
#if TRAINING
	(*_file) << column;
#endif
}

void TrainingController::AddGameResult(int code, char* description)
{
#if TRAINING
	(*_file) << endl << code << endl << description << endl;
#endif
}

void TrainingController::Shutdown()
{
#if TRAINING
	_file->close();
#endif
}
