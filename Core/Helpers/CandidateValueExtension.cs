using Core.Data;
using System.Runtime.CompilerServices;

namespace Core.Helpers
{
    static class CandidateValueExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CandidateValue ToCandidateValue(this InputValue inputValue)
        {
            return (CandidateValue) (1 << inputValue);
        }
    }
}
