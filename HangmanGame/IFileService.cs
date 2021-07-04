using System.Collections.Generic;

namespace HangmanGame
{
    interface IFileService
    {
        Solution GetRandomSolution();
        void SaveScore(Score score);
        List<Score> GetScores();
    }
}
