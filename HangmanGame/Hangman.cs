using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanGame
{
    class Hangman
    {
        public int LifePoints { get; private set; }
        private string _capital;
        private List<(bool visible, char letter)> _solutionHangman;
        public int LettersLeftHidden { get; private set; }
        public Hangman(string capital)
        {
            LifePoints = 5;
            _capital = capital;
            LettersLeftHidden = capital.Length;
            _solutionHangman = new List<(bool visible, char letter)>();
            for (int i = 0; i < capital.Length; i++)
            {
                if (capital[i] == ' ')
                {
                    _solutionHangman.Add((true, capital[i]));
                    LettersLeftHidden--;
                }
                else
                    _solutionHangman.Add((false, capital[i]));
            }
        }
        public void PrintPuzzle()
        {
            Console.Write('\t');
            foreach (var character in _solutionHangman)
            {
                if (character.visible == true)
                    Console.Write(character.letter);
                else
                    Console.Write("_");
            }
            Console.WriteLine();
        }
        public void UncoverPuzzle()
        {
            for (int i = 0; i < _solutionHangman.Count; i++)     //all letters are made visible
            {
                if (_solutionHangman[i].visible == false)
                {
                    _solutionHangman[i] = (true, _solutionHangman[i].letter);
                }
            }
        }
        public bool GuessLetter(char letter)
        {
            bool letterFound = false;
            for (int i=0; i<_solutionHangman.Count; i++)
            {
                if (_solutionHangman[i].visible == false)
                {
                    if (char.ToLower(_solutionHangman[i].letter) == char.ToLower(letter))
                    {
                        letterFound = true;
                        _solutionHangman[i] = (true, _solutionHangman[i].letter); //quessed letter is visible now
                        LettersLeftHidden--;
                    }
                }
            }
            if (!letterFound)
                LifePoints--;
            return letterFound;
        }
        public bool GuessWord(string word)
        {
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
        public string GetHangmanArt()
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
            return hangmanArt;
        }
    }
}