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
        private readonly IFileService _fileService;
        private Solution _solution;
        private IHangman _hangman;
        private readonly IScoreService _scoreService;
        private List<char> _notInWordList;
        private int _letterGuessCount;
        private int _wordGuessCount;
        private int _guessingTime;

        public HangmanGameRound()
        {
            _fileService = new FileService();
            _solution = _fileService.GetRandomSolution();
            _hangman = new Hangman(_solution.Capital);
            _scoreService = new ScoreService();
            _notInWordList = new List<char>();
            _letterGuessCount = 0;
            _wordGuessCount = 0;
            _guessingTime = 0;
        }

        public void NewRound()
        {
            bool gameUnfinished = true;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            while (gameUnfinished)
            {
                PrintGameInfo();

                _hangman.PrintPuzzle();

                _hangman.PrintHangmanArt();

                if (_hangman.LifePoints == 1)
                    _solution.PrintHint();

                Guessing();

                if (_hangman.LifePoints <= 0)
                {
                    stopWatch.Stop();
                    _guessingTime = stopWatch.Elapsed.Seconds + (stopWatch.Elapsed.Minutes * 60);
                    Losing();
                    gameUnfinished = false;
                }
                else if (_hangman.LettersLeftHidden <= 0)
                {
                    stopWatch.Stop();
                    _guessingTime = stopWatch.Elapsed.Seconds + (stopWatch.Elapsed.Minutes * 60);
                    Winning();
                    gameUnfinished = false;
                }

                Thread.Sleep(1000); //delay for 1 second
                Console.Clear();
            }
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
            Console.Write("\tCategory: Capitals");
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
                        _letterGuessCount++;
                    }
                    else
                    {
                        Console.WriteLine("\n\tYou missed! You are losing 1 life point!");
                        _notInWordList.Add(letter);
                        _letterGuessCount++;
                    }
                    return;
                }
                else
                {
                    Console.Write("\n\tThis wasn't a letter. Try again: ");
                }
            }
        }

        private void GuessWord()
        {
            bool invalidInput = true;

            Console.Write("\n\tWhat is your answer?  ");

            while (invalidInput)
            {
                string word = Console.ReadLine().Trim();

                if (!string.IsNullOrWhiteSpace(word))
                {
                    invalidInput = false;
                    _wordGuessCount++;

                    if (_hangman.GuessWord(word))
                    {
                        Console.WriteLine("\n\tCongratulations! You were right!");
                    }
                    else
                    {
                        Console.WriteLine("\n\tYou missed! You are losing 2 life points!");
                    }
                }
                else
                {
                    Console.Write("\n\tYou left a blank space. Please try again:  ");
                }
            }
        }

        private void Guessing()
        {
            bool invalidInput = true;

            Console.WriteLine("\n\tDo you want to try to guess one letter or whole word?");
            Console.WriteLine("\tWrite '1' to guess one letter");
            Console.WriteLine("\tWrite '2' to guess whole word (failure results in losing 2 life points)");
            Console.Write("\tYou choose to: ");

            while(invalidInput)
            {
                char choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        {
                            Console.WriteLine();
                            GuessLetter();
                            invalidInput = false;
                        }
                        break;
                    case '2':
                        {
                            Console.WriteLine();
                            GuessWord();
                            invalidInput = false;
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("\n\tPlease use only '1' or '2'");
                            Console.Write("\t: ");
                        }
                        break;
                }
            }
        }

        private void Winning()
        {
            _hangman.PrintHangmanArt();

            Console.WriteLine("\n\tYou won!");

            _hangman.PrintPuzzle();

            Console.WriteLine($"\n\tYou guessed the capital after {_letterGuessCount} letters. It took you {_guessingTime} seconds.");

            _scoreService.SaveScore(DateTime.Now, _guessingTime, _wordGuessCount + _letterGuessCount, _solution.Capital);
            _scoreService.PrintBestScores();

            Console.Write("\n\tPress any key to continue...");
            Console.ReadKey();
        }

        private void Losing()
        {
            _hangman.PrintHangmanArt();

            Console.WriteLine("\n\tYou lost!");
            Console.WriteLine($"\tThe answer was: {_solution.Capital}");
            Console.WriteLine($"\n\tYou failed to guess the capital after {_letterGuessCount} letters. It took you {_guessingTime} seconds.");

            _scoreService.PrintBestScores();

            Console.Write("\n\tPress any key to continue...");
            Console.ReadKey();
        }
    }
}
