namespace Core.Data
{
    static class CandidateValueExtension
    {
        public static CandidateValue ToCandidateValue(this InputValue inputValue)
        {
            return (CandidateValue) (1 << (int) inputValue);
        }
    }
}
