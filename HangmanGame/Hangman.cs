using System;
using System.Collections.Generic;
using System.Text;
using HangmanGame.Utilities;
using System.Linq;

namespace HangmanGame
{
    class Hangman : IHangman
    {
        public int LifePoints { get; private set; }
        private readonly string _capital;
        private readonly List<Letter> _letters;
        public int LettersLeftHidden { get; private set; }

        public Hangman(string capital)
        {
            Guard.CheckNullOrEmpty(capital, nameof(capital));

            LifePoints = 5;
            _capital = capital;
            LettersLeftHidden = capital.Length;
            _letters = new List<Letter>();

            InitLetters();
        }

        public void PrintPuzzle()
        {
            Console.Write('\t');

            foreach (var letter in _letters)
            {
                if (letter.IsVisible == true)
                {
                    Console.Write(letter.Value);
                }
                else
                {
                    Console.Write("_");
                }
            }

            Console.WriteLine();
        }
        
        public bool GuessLetter(char letter)
        {
            bool result = false;
            var letterFound = _letters.FirstOrDefault(l => char.ToLower(l.Value) == char.ToLower(letter));

            if (letterFound != null)
            {
                result = true;

                _letters.ForEach(l =>
                {
                    if (char.ToLower(l.Value) == char.ToLower(letter))
                    {
                        l.IsVisible = true;
                    }
                });
            }
            else
            {
                LifePoints--;
                result = false;
            }

            return result;
        }

        public bool GuessWord(string word)
        {
            Guard.CheckNullOrEmpty(word, nameof(word));

            if(word.ToLower() == _capital.ToLower())
            {
                UncoverPuzzle();
                LettersLeftHidden = 0;
                return true;
            }
            else
            {
                LifePoints -= 2;
                return false;
            }
        }

        public void PrintHangmanArt()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\t _________\n");
            sb.Append("\t |      \\|\n");
            switch (LifePoints)
            {
                case 5:
                    sb.Append("\t         |\n");
                    sb.Append("\t         |\n");
                    sb.Append("\t         |\n");
                    sb.Append("\t         |\n");
                    break;
                case 4:
                    sb.Append("\t O       |\n");
                    sb.Append("\t |       |\n");
                    sb.Append("\t         |\n");
                    sb.Append("\t         |\n");
                    break;
                case 3:
                    sb.Append("\t O       |\n");
                    sb.Append("\t/|       |\n");
                    sb.Append("\t         |\n");
                    sb.Append("\t         |\n");
                    break;
                case 2:
                    sb.Append("\t O       |\n");
                    sb.Append("\t/|\\      |\n");
                    sb.Append("\t         |\n");
                    sb.Append("\t         |\n");
                    break;
                case 1:
                    sb.Append("\t O       |\n");
                    sb.Append("\t/|\\      |\n");
                    sb.Append("\t/        |\n");
                    sb.Append("\t         |\n");
                    break;
                case 0:
                    sb.Append("\t O       |\n");
                    sb.Append("\t/|\\      |\n");
                    sb.Append("\t/ \\      |\n");
                    sb.Append("\t         |\n");
                    break;
                default:    //for those who ended with -1 life points (yes you can)
                    sb.Append("\t(_)      |\n");
                    sb.Append("\t/|\\      |\n");
                    sb.Append("\t/ \\      |\n");
                    sb.Append("\tTIS BUT A|\n");
                    sb.Append("\tSCRATCH  |\n");
                    break;
            }
            sb.Append("\t===========\n");
            string hangmanArt = sb.ToString();

            Console.WriteLine(hangmanArt);
        }

        private void UncoverPuzzle()
        {
            foreach (var letter in _letters)
            {
                if (letter.IsVisible == false)
                {
                    letter.IsVisible = true;
                }
            }
        }

        private void InitLetters()
        {
            foreach (var letter in _capital)
            {
                if(letter == ' ')
                {
                    _letters.Add(new Letter(letter, true));
                }
                else
                {
                    _letters.Add(new Letter(letter, false));
                }
            }
        }
    }
}