using System;
using HangmanGame.Utilities;

namespace HangmanGame
{
    class Solution
    {
        public string Country { get; set; }
        public string Capital { get; set; }

        public Solution(string country, string capital)
        {
            Guard.CheckNullOrEmpty(country, nameof(country));
            Guard.CheckNullOrEmpty(capital, nameof(capital));

            Country = country;
            Capital = capital;
        }

        public void PrintHint()
        {
            Console.WriteLine($"\n\tHint: The capital of {Country}");
        }
    }
}
