using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanGame
{
    class Hangman
    {
        private int lifePoints;
        public Hangman()
        {
            lifePoints = 5;
        }
        private bool NewRound()
        {
            Console.WriteLine("[------------- HANGMAN -------------]");
            bool solved = false;

            while(lifePoints>0 || solved)
            {

                //quessing
                solved = true;

                if(lifePoints==0)
                {
                    Console.WriteLine("You lose");
                    break;
                } 
                else if(solved)
                {
                    Console.WriteLine("You win");
                    break;
                }
            }

            for (; ; )
            {
                Console.WriteLine("Do you want to give it another try? (y/n)");
                string keepPlaying = Console.ReadLine();
                if (keepPlaying == "y")
                    return true;
                else if (keepPlaying == "n")
                    return false;
                else
                    Console.WriteLine("Please use only 'y' or 'n'");
            }
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
