using System;

namespace HangmanGame
{
    class HangmanGame
    {
        private bool keepPlaying;

        public HangmanGame()
        {
            keepPlaying = true;
        }

        public void StartGame()
        {
            while(keepPlaying)
            {
                Console.WriteLine("\n\t1. Play");
                Console.WriteLine("\t2. Exit");

                Console.Write("\t:");
                char choice = Console.ReadKey().KeyChar;

                switch(choice)
                {
                    case '1':
                        {
                            Console.Clear();
                            var gameRound = new HangmanGameRound();
                            gameRound.NewRound();
                        }
                        break;
                    case '2':
                        {
                            Console.Clear();
                            Console.WriteLine("\n\tThank you for playing");
                            keepPlaying = false;
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("\n\tInvalid input! Please use avaible options!");
                        }
                        break;
                }
            }
        }
    }
}
