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


        private static Dictionary<CandidateValue, IReadOnlyList<InputValue>> _candidatesAsList
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
    }
}
