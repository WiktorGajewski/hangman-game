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
                try
                {
                    var gameRound = new HangmanGameRound();
                    gameRound.NewRound();   //starting a round
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred while running the program.");
                    Console.WriteLine(e);
                    break;
                }

                for (; ; )  //question about restarting the program
                {
                    Console.WriteLine("\n\tDo you want to give it another try? (y/n)");
                    Console.Write("\t");
                    char anotherTry = Console.ReadKey().KeyChar;
                    if (anotherTry == 'y')
                        break;
                    else if (anotherTry == 'n')
                    {
                        keepPlaying = false;
                        break;
                    }
                    else
                        Console.WriteLine("\tPlease use only 'y' or 'n'");
                }

                Console.Clear();
            }
        }
    }
}
