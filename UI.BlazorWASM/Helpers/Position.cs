namespace UI.BlazorWASM.Helpers
{
    public readonly struct Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        readonly public int X;
        readonly public int Y;
    }
}
