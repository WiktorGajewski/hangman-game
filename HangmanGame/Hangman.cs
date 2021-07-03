using System;

namespace HangmanGame
{
    class Hangman
    {
        public int lifePoints { get; private set; }
        public Hangman(string capital)
        {
            lifePoints = 5;
        }
    }
}
