namespace UI.BlazorWASM.Helpers
{
    public readonly struct Position
    {
        readonly public int X;
        readonly public int Y;
        public int Block => (Y / 3) * 3 + (X / 3);

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString() => $"r{Y+1}c{X+1}";
    }
}
