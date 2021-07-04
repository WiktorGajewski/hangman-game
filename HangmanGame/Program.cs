using System;

namespace HangmanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var game = new HangmanGame();
                game.StartGame();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while running the program.");
                Console.WriteLine(e.Message);
            }
        }
    }
}
