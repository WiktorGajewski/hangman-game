using System;

namespace HangmanGame
{
    interface IScoreService
    {
        void SaveScore(DateTime date, int quessingTime, int quessingTriesCount, string quessedWord);
        void PrintBestScores();
    }
}
