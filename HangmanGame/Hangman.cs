using System;
using System.IO;
using System.Linq;

namespace HangmanGame
{
    class Hangman
    {
        private int lifePoints;
        public Hangman()
        {
            lifePoints = 5;
        }
        private string drawRandomLineFromFile()
        {
            string filePath = "Assets/countries_and_capitals.txt";
            if(File.Exists(filePath))
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
        private bool NewRound()
        {
            Console.WriteLine("[------------- \tHANGMAN\t -------------]");
            bool quessedWord = false;
            lifePoints = 5;

            string[] solution = drawRandomLineFromFile().Split(" | ");
            string country = solution[0];
            string capital = solution[1];
            
            while(lifePoints>0 || quessedWord)
            {

                //quessing
                quessedWord = true;

                if(lifePoints<=0)
                {
                    Console.WriteLine("You lose");
                    break;
                } 
                else if(quessedWord)
                {
                    Console.WriteLine("You win");
                    break;
                }
            }

            for (; ; )  //question about restarting program
            {
                Console.WriteLine("Do you want to give it another try? (y/n)");
                string keepPlaying = Console.ReadLine();
                if (keepPlaying == "y")
                    return true;
                else if (keepPlaying == "n")
                    return false;
                else
                    Console.WriteLine("Please use only 'y' or 'n'");
            }           //
        }
        public void StartGame()
        {
            bool keepOn = true;
            while(keepOn)
            {
                if (!NewRound())
                    keepOn = false;
                Console.Clear();
            }
        }
    }
}
