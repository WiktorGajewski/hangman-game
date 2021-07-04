using System;

namespace HangmanGame
{
    class Score
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int QuessingTime { get; set; }
        public int QuessingTries { get; set; }
        public string QuessedWord { get; set; }

        public Score(string name, DateTime date, int quessingTime, int quessingTries, string quessedWord)
        {
            Name = name;
            Date = date;
            QuessingTime = quessingTime;
            QuessingTries = quessingTries;
            QuessedWord = quessedWord;
        }
        public string PrintScore()
        {
            return $"{Name}|{Date.ToString("dd.MM.yyyy HH:mm")}|{QuessingTime.ToString()}|{QuessingTries.ToString()}|{QuessedWord}";
        }
        public string PrintScoreInConsole()
        {
            return $"{Name,16}\t{Date.ToString("dd.MM.yyyy HH:mm"),16}\t{QuessingTime.ToString(),14}\t{QuessingTries.ToString(),14}\t{QuessedWord,16}";
        }
    }
}
