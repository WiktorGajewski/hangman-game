using System;
using System.Collections.Generic;
using System.Linq;

namespace HangmanGame
{
    class ScoreService : IScoreService
    {
        private readonly IFileService _fileService;

        public ScoreService()
        {
            _fileService = new FileService();
        }

        public void SaveScore(DateTime date, int quessingTime, int quessingTriesCount, string quessedWord)
        {
            bool invalidInput = true;

            Console.Write("\n\tWhat is your name? ");

            while (invalidInput)
            {
                string name = Console.ReadLine().Trim();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    invalidInput = false;

                    Score score = new Score(name, date, quessingTime, quessingTriesCount, quessedWord);

                    _fileService.SaveScore(score);

                    Console.WriteLine("\tYour score has been saved.");
                }
                else
                {
                    Console.Write("\n\tYou left a blank space. Please try again:  ");
                }
            }
        }

        public void PrintBestScores()
        {
            Console.WriteLine("\n\tBest scores (fastest completion):");

            var scores = _fileService.GetScores();

            if (scores.Count <= 0)
            {
                Console.WriteLine("\tNo score has been saved yet.");
            }
            else
            {
                var bestScores = scores.OrderBy(s => s.GuessingTime).ThenBy(s => s.GuessingTries).Take(10);

                Console.WriteLine($"\t{"Name",16}\t{"Date",16}\t{"Guessing time",14}\t{"Guessing tries",14}\t{"Guessed word",16}");

                foreach (var score in bestScores)
                {
                    score.PrintScore();
                }
            }
        }
    }
}
