using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace HangmanGame
{
    class ScoreManager
    {
        public ScoreManager()
        {
        }
        public void SaveScore(DateTime date, int quessingTime, int quessingTriesCount, string quessedWord)
        {
            Console.Write("\n\tWhat is your name? ");
            string name = Console.ReadLine();
            Score score = new Score(name, date, quessingTime, quessingTriesCount, quessedWord);

            string filePath = "Assets/results.txt";
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(score.PrintScore());
            }
            Console.WriteLine("\tYour score has been saved.");
        }
        public void PrintBestScores()
        {
            Console.WriteLine("\n\tBest scores (fastest completion):");
            string filePath = "Assets/results.txt";
            if (File.Exists(filePath))
            {
                int linesCount = File.ReadLines(filePath).Count();

                List<Score> scores = new List<Score>();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    string[] splitLine;
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            splitLine = line.Split("|");
                            Score score = new Score
                            (
                                splitLine[0],
                                DateTime.ParseExact(splitLine[1], "dd.MM.yyyy HH:mm", null),
                                Convert.ToInt32(splitLine[2]),
                                Convert.ToInt32(splitLine[3]),
                                splitLine[4]
                            );
                            scores.Add(score);
                        }
                        catch { }   //skip lines that are in wrong format
                    }
                }
                var bestScores = scores.OrderBy(s => s.QuessingTime).ThenBy(s => s.QuessingTries).Take(10).ToList();
                Console.WriteLine($"\t{"Name",16}\t{"Date",16}\t{"Guessing time",14}\t{"Guessing tries",14}\t{"Guessed word",16}");
                foreach (var score in bestScores)
                    Console.WriteLine($"\t{score.PrintScoreInConsole()}");
            }
            else
                Console.WriteLine("\tNo score has been saved yet.");
        }
    }
}
