namespace HangmanGame
{
    class Letter
    {
        public char Value { get; set; }
        public bool IsVisible { get; set; }

        public Letter(char value, bool isVisible)
        {
            Value = value;
            IsVisible = isVisible;
        }
    }
}
