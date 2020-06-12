namespace UI.BlazorWASM.Helpers
{
    public readonly struct Coords
    {
        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        readonly public int X;
        readonly public int Y;
    }
}
