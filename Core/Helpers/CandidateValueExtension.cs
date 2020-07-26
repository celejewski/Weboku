using Core.Data;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Core.Helpers
{
    public static class CandidateValueExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CandidateValue ToCandidateValue(this InputValue inputValue)
        {
            return (CandidateValue) (1 << inputValue);
        }

        private static readonly Dictionary<CandidateValue, IReadOnlyList<InputValue>> _candidatesAsList
            = new Dictionary<CandidateValue, IReadOnlyList<InputValue>>();
        public static IReadOnlyList<InputValue> ToList(this CandidateValue candidates)
        {
            if (!_candidatesAsList.ContainsKey(candidates))
            {
                var result = new List<InputValue>();
                foreach( var value in InputValue.NonEmpty )
                {
                    if ((candidates & value.ToCandidateValue()) == value.ToCandidateValue())
                    {
                        result.Add(value);
                    }
                }
                _candidatesAsList[candidates] = result.AsReadOnly();
            }
            return _candidatesAsList[candidates];
        }

        private static readonly Dictionary<CandidateValue, int> _candidatesCount = new Dictionary<CandidateValue, int>(1024);
        public static int Count(this CandidateValue candidates)
        {
            if( !_candidatesCount.ContainsKey(candidates) )
            {
                int count = 0;
                for( int value = 1; value < 10; value++ )
                {
                    var mask = (CandidateValue) (1 << value);
                    if( (candidates & mask) == mask)
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
