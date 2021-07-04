using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace HangmanGame
{
    class FileService : IFileService
    {
        private const string inputFilePath = "Assets/countries_and_capitals.txt";
        private const string saveFilePath = "Assets/results.txt";

        public Solution GetRandomSolution()
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"File '{inputFilePath}' could not be found");
                throw new FileNotFoundException();
            }


            int linesCount = File.ReadLines(inputFilePath).Count();
            var random = new Random();
            int randomNumber = random.Next(1, linesCount) - 1;

            string[] solution = File.ReadLines(inputFilePath)
                .Skip(randomNumber)
                .First()
                .Split(" | ");

            return new Solution(solution[0], solution[1]);
        }

        public void SaveScore(Score score)
        {
            using (StreamWriter sw = File.AppendText(saveFilePath))
            {
                sw.WriteLine(score.GetScoreAsString());
            }
        }

        public List<Score> GetScores()
        {
            List<Score> scores = new List<Score>();

            if (!File.Exists(saveFilePath))
            {
                return scores;
            }

            using (StreamReader sr = new StreamReader(saveFilePath))
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

            return scores;
        }
    }
}
