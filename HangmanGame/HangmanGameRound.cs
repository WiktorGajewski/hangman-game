using System;
using System.IO;
using System.Linq;

namespace HangmanGame
{
    class HangmanGameRound
    {
        public HangmanGameRound()
        {
        }
        private string takeRandomLineFromFile()
        {
            string filePath = "Assets/countries_and_capitals.txt";
            if (File.Exists(filePath))
            {
                int linesCount = File.ReadLines(filePath).Count();
                var random = new Random();
                int randomNumber = random.Next(1, linesCount);

                string line = File.ReadLines(filePath).Skip(--randomNumber).Take(1).First();
                return line;
            }
            else
            {
                Console.WriteLine($"File '{filePath}' could not be found");
                throw new FileNotFoundException();
            }
        }
        public void NewRound()
        {
            string[] solution = takeRandomLineFromFile().Split(" | ");  //picking random country (and it's capital)
            string country = solution[0];
            string capital = solution[1];

            Console.WriteLine("[------------- \tHANGMAN\t -------------]");
            var hangman = new Hangman(capital);

            while (true)
            {

                //quessing
                break;

            }
        }
    }
}
