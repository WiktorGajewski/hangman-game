namespace HangmanGame
{
    interface IHangman
    {
        int LifePoints { get; }
        int LettersLeftHidden { get; }

        void PrintPuzzle();
        bool GuessLetter(char letter);
        bool GuessWord(string word);
        void PrintHangmanArt();
    }
}
