namespace Core.Data
{
    static class GridV2Helper
    {
        public static CandidateValue ToCandidateValue(this InputValue inputValue)
        {
            return (CandidateValue) (1 << (int) inputValue);
        }
    }
}
