using System;

namespace HangmanGame.Utilities
{
    static class Guard
    {
        public static void CheckNull<T>(T obj, string name) where T: class
        {
            if( obj is null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void CheckNullOrEmpty(string str, string name)
        {
            if(string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException(name);
            }
        }

        public static void CheckBelowZero(int number, string name)
        {
            if(number < 0)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }
    }
}
