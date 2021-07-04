using System;
using HangmanGame.Utilities;

namespace HangmanGame
{
    class Score
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int GuessingTime { get; set; }
        public int GuessingTries { get; set; }
        public string GuessedWord { get; set; }

        public Score(string name, DateTime date, int guessingTime, int guessingTries, string guessedWord)
        {
            Guard.CheckNullOrEmpty(name, nameof(name));
            Guard.CheckNullOrEmpty(guessedWord, nameof(guessedWord));
            Guard.CheckBelowZero(guessingTime, nameof(guessingTime));
            Guard.CheckBelowZero(guessingTries, nameof(guessingTries));

            Name = name;
            Date = date;
            GuessingTime = guessingTime;
            GuessingTries = guessingTries;
            GuessedWord = guessedWord;
        }

        public string GetScoreAsString()
        {
            return $"{Name}|{Date.ToString("dd.MM.yyyy HH:mm")}|{GuessingTime.ToString()}|{GuessingTries.ToString()}|{GuessedWord}";
        }

        public void PrintScore()
        {
            Console.WriteLine($"\t{Name,16}\t{Date.ToString("dd.MM.yyyy HH:mm"),16}\t{GuessingTime.ToString(),14}\t{GuessingTries.ToString(),14}\t{GuessedWord,16}");
        }
    }
}
