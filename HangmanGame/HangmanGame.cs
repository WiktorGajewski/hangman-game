using System;

namespace HangmanGame
{
    class HangmanGame
    {
        public HangmanGame()
        {
        }
        public void StartGame()
        {
            bool keepPlaying = true;
            while(keepPlaying)
            {
                var gameRound = new HangmanGameRound();
                try
                {
                    gameRound.NewRound();   //starting a round
                }
                catch
                {
                    Console.WriteLine("An error occurred while running the program.");
                    break;
                }

                for (; ; )  //question about restarting the program
                {
                    Console.WriteLine("Do you want to give it another try? (y/n)");
                    string anotherTry = Console.ReadLine();
                    if (anotherTry == "y")
                        break;
                    else if (anotherTry == "n")
                    {
                        keepPlaying = false;
                        break;
                    }
                    else
                        Console.WriteLine("Please use only 'y' or 'n'");
                }

                Console.Clear();
            }
        }
    }
}
