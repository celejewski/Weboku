using System;

namespace Core.Data
{
    [Flags]
    public enum Candidates : short
    {
        None = 0,
        One = 1 << 1,
        Two = 1 << 2,
        Three = 1 << 3,
        Four = 1 << 4,
        Five = 1 << 5,
        Six = 1 << 6,
        Seven = 1 << 7,
        Eight = 1 << 8,
        Nine = 1 << 9,
        All = One | Two | Three | Four | Five | Six | Seven | Eight | Nine
    }
}
