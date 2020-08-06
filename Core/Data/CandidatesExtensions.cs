using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Core.Data
{
    public static class CandidatesExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Candidates AsCandidates(this InputValue inputValue)
        {
            return (Candidates) (1 << (inputValue - 1));
        }

        private static readonly IReadOnlyList<InputValue>[] _candidatesAsList
            = new IReadOnlyList<InputValue>[512];
        public static IReadOnlyList<InputValue> ToInputValues(this Candidates candidates)
        {
            return _candidatesAsList[(int) candidates] ??= ToInputValuesCalculate(candidates);
        }

        private static IReadOnlyList<InputValue> ToInputValuesCalculate(Candidates candidates)
        {
            var result = new List<InputValue>();
            foreach( var value in InputValue.NonEmpty )
            {
                if( (candidates & value.AsCandidates()) == value.AsCandidates() )
                {
                    result.Add(value);
                }
            }
            return result.AsReadOnly();
        }

        private static readonly int?[] _candidatesCount = new int?[512];
        public static int Count(this Candidates candidates)
        {
            return _candidatesCount[(int) candidates] ??= CalculateCount(candidates);
        }

        private static int CalculateCount(Candidates candidates)
        {
            int count = 0;
            for( int value = 0; value < 9; value++ )
            {
                var mask = (Candidates) (1 << value);
                if( (candidates & mask) == mask )
                {
                    count++;
                }
            }
            return count;
        }

        static CandidatesExtensions()
        {
            for( int i = 0; i < 512; i++ )
            {
                var candidates = (Candidates) i;
                candidates.ToInputValues();
                candidates.Count();
            }
        }
    }
}
