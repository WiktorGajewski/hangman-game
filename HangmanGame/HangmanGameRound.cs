using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace HangmanGame
{
    class HangmanGameRound
    {
        private string _country;
        private string _capital;
        private Hangman _hangman;
        private ScoreManager _scoreManager;
        private List<char> _notInWordList;
        private int _quessingLetterCount;
        private int _quessingWordCount;
        private int _quessingTime;
        public HangmanGameRound()
        {
            string[] solution = TakeRandomLineFromFile().Split(" | ");  //picking random country (and it's capital)
            _country = solution[0];
            _capital = solution[1];
            _hangman = new Hangman(_capital);
            _scoreManager = new ScoreManager();
            _notInWordList = new List<char>();
            _quessingLetterCount = 0;
            _quessingWordCount = 0;
            _quessingTime = 0;
        }
        private string TakeRandomLineFromFile()     //take random country-capital pair
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
            Console.WriteLine($"\n\tHint: The capital of {_country}");
        }
        private void PrintGameInfo()
        {
            Console.WriteLine("\t[---------------- HANGMAN ----------------]\n");
            Console.WriteLine($"\tYour life point: {_hangman.LifePoints}");
            Console.Write($"\tNot in word:");
            foreach(char letter in _notInWordList)
            {
                Console.Write($" {letter} ");
            }
            Console.WriteLine('\n');
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
                    {
                        Console.WriteLine("\n\tFound it!");
                        _quessingLetterCount++;
                    }
                    else
                    {
                        Console.WriteLine("\n\tYou missed! You are losing 1 life point!");
                        _notInWordList.Add(letter);
                        _quessingLetterCount++;
                    }
                    return;
                }
                Console.Write("\n\tThis wasn't a letter. Try again: ");
            }
        }
        private void GuessWord()
        {
            Console.Write("\n\tWhat is your answer?  ");
            string word = Console.ReadLine();
            _quessingWordCount++;
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
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            while (true)        //quessing till you win or lose
            {
                PrintGameInfo();
                Console.Write("\tCategory: Capitals");
                _hangman.PrintPuzzle();
                Console.WriteLine(_hangman.GetHangmanArt());
                if (_hangman.LifePoints == 1)
                    PrintHint();
                Guessing();

                if(_hangman.LifePoints <= 0)
                {
                    Console.WriteLine(_hangman.GetHangmanArt());
                    Console.WriteLine("\n\tYou lost!");
                    Console.WriteLine($"\tThe answer was: {_capital}");
                    stopWatch.Stop();
                    _quessingTime = stopWatch.Elapsed.Seconds + (stopWatch.Elapsed.Minutes * 60);
                    Console.WriteLine($"\n\tYou failed to guess the capital after {_quessingLetterCount} letters. It took you {_quessingTime} seconds.");
                    _scoreManager.PrintBestScores();
                    break;
                } else if(_hangman.LettersLeftHidden <= 0)
                {
                    Console.WriteLine(_hangman.GetHangmanArt());
                    Console.WriteLine("\n\tYou won!");
                    _hangman.PrintPuzzle();
                    stopWatch.Stop();
                    _quessingTime = stopWatch.Elapsed.Seconds + (stopWatch.Elapsed.Minutes * 60);
                    Console.WriteLine($"\n\tYou guessed the capital after {_quessingLetterCount} letters. It took you {_quessingTime} seconds.");
                    _scoreManager.SaveScore(DateTime.Now, _quessingTime, _quessingWordCount + _quessingLetterCount, _capital);
                    _scoreManager.PrintBestScores();
                    break;
                }
                Thread.Sleep(1000); //delay for 1 second
                Console.Clear();
            }
        }
    }
}
