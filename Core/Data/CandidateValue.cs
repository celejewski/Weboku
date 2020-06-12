using System;

namespace Core.Data
{
    [Flags]
    enum CandidateValue : short
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 4,
        Four = 8,
        Five = 16,
        Six = 32,
        Seven = 64,
        Eight = 128,
        Nine = 256,
        All = One | Two | Three | Four | Five | Six | Seven | Eight | Nine
    }
}
