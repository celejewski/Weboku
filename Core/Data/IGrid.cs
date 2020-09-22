namespace Core.Data
{
    public interface IGrid
    {
        bool GetIsGiven(Position position);
        void SetIsGiven(Position position, bool value);

        InputValue GetValue(Position position);
        void SetValue(Position position, InputValue value);
        bool HasValue(Position position);

        bool HasCandidate(Position position, InputValue value);
        Candidates GetCandidates(Position position);

        void AddCandidate(Position position, InputValue value);
        void RemoveCandidate(Position position, InputValue value);
        void ToggleCandidate(Position position, InputValue value);
        bool IsCandidateLegal(Position position, InputValue value);
        void FillAllLegalCandidates();
        void ClearAllCandidates();
        void ClearCandidates(Position position);

        IGrid Clone();
    }
}
