using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace HangmanGame
{
    class HangmanGameRound
    {
        private string _country;
        private string _capital;
        private Hangman _hangman;
        public HangmanGameRound()
        {
            string[] solution = TakeRandomLineFromFile().Split(" | ");  //picking random country (and it's capital)
            _country = solution[0];
            _capital = solution[1];
            _hangman = new Hangman(_capital);
        }
        private string TakeRandomLineFromFile()
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
        private void PrintHint()
        {
            Console.WriteLine($"\n\tThe capital of {_country}");
        }
        private void PrintGameInfo()
        {
            Console.WriteLine("\t[---------------- HANGMAN ----------------]\n");
            Console.WriteLine($"\tYour life point: {_hangman.LifePoints}\n");
        }
        private void GuessLetter()
        {
            Console.Write("\n\tWhat letter do you choose? ");
            
            while (true)
            {
                char letter = Console.ReadKey().KeyChar;
                if (char.IsLetter(letter))
                {
                    if (_hangman.GuessLetter(letter))
                        Console.WriteLine("\n\tFound it!");
                    else
                        Console.WriteLine("\n\tYou missed! You are losing 1 life point!");
                    return;
                }
                Console.Write("\n\tThis wasn't a letter. Try again: ");
            }
        }
        private void GuessWord()
        {
            Console.Write("\n\tWhat is your answer?  ");
            string word = Console.ReadLine();

            if (_hangman.GuessWord(word))
                Console.WriteLine("\n\tCongratulations! You were right!");
            else
                Console.WriteLine("\n\tYou missed! You are losing 2 life points!");
        }
        private void Guessing()
        {
            Console.WriteLine("\n\tDo you want to try to guess one letter or whole word?");
            Console.WriteLine("\tWrite '1' to guess one letter");
            Console.WriteLine("\tWrite '2' to guess whole word (failure results in losing 2 life points)");
            Console.Write("\tYou choose to: ");
            char choice = Console.ReadKey().KeyChar;
            while(true)
            {
                if (choice == '1')
                {
                    Console.WriteLine();
                    GuessLetter();
                    break;
                }
                else if (choice == '2')
                {
                    Console.WriteLine();
                    GuessWord();
                    break;
                }
                else
                {
                    Console.WriteLine("\n\tPlease use only '1' or '2'");
                    Console.Write("\t: ");
                    choice = Console.ReadKey().KeyChar;
                }
            }
        }
        public void NewRound()
        {
            while (true)    //quessing till you win or lose
            {
                PrintGameInfo();
                Console.Write("\tCategory: Capitals");
                _hangman.PrintPuzzle();
                if (_hangman.LifePoints == 1)
                    PrintHint();
                Guessing();

                if(_hangman.LifePoints <= 0)
                {
                    Console.WriteLine("\n\tYou lost!");
                    Console.WriteLine($"\tThe answer was: {_capital}");
                    break;
                } else if(_hangman.LettersLeftHidden <= 0)
                {
                    Console.WriteLine("\n\tYou won!");
                    _hangman.PrintPuzzle();
                    break;
                }
                Thread.Sleep(3000); //delay for 3 seconds
                Console.Clear();
            }
        }
    }
}
