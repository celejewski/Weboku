using Core.Data;

namespace Core.Helpers
{
    internal static class CandidateValueExtension
    {
        public static CandidateValue ToCandidateValue(this InputValue inputValue)
        {
            return (CandidateValue) (1 << inputValue);
        }
    }
}
