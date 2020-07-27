using Core.Data;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Core.Helpers
{
    public static class CandidateValueExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Candidates ToCandidateValue(this InputValue inputValue)
        {
            return (Candidates) (1 << inputValue);
        }

        private static readonly Dictionary<Candidates, IReadOnlyList<InputValue>> _candidatesAsList
            = new Dictionary<Candidates, IReadOnlyList<InputValue>>();
        public static IReadOnlyList<InputValue> ToList(this Candidates candidates)
        {
            if( !_candidatesAsList.ContainsKey(candidates) )
            {
                var result = new List<InputValue>();
                foreach( var value in InputValue.NonEmpty )
                {
                    if( (candidates & value.ToCandidateValue()) == value.ToCandidateValue() )
                    {
                        result.Add(value);
                    }
                }
                _candidatesAsList[candidates] = result.AsReadOnly();
            }
            return _candidatesAsList[candidates];
        }

        private static readonly Dictionary<Candidates, int> _candidatesCount = new Dictionary<Candidates, int>(1024);
        public static int Count(this Candidates candidates)
        {
            if( !_candidatesCount.ContainsKey(candidates) )
            {
                int count = 0;
                for( int value = 1; value < 10; value++ )
                {
                    var mask = (Candidates) (1 << value);
                    if( (candidates & mask) == mask )
                    {
                        count += 1;
                    }
                }
                _candidatesCount[candidates] = count;
            }
            return _candidatesCount[candidates];
        }
    }
}
