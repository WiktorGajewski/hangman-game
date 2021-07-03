using System;
using System.Collections.Generic;

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
            Console.Write("\t");
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
                        _solutionHangman[i] = (true, _solutionHangman[i].letter); //letter is visible now
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
    }
}
