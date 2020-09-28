namespace Core.Data
{
    public interface IGrid
    {
        bool GetIsGiven(Position position);
        void SetIsGiven(Position position, bool value);

        Value GetValue(Position position);
        void SetValue(Position position, Value value);
        bool HasValue(Position position);

        bool HasCandidate(Position position, Value value);
        Candidates GetCandidates(Position position);

        void AddCandidate(Position position, Value value);
        void RemoveCandidate(Position position, Value value);
        void ToggleCandidate(Position position, Value value);
        bool IsCandidateLegal(Position position, Value value);
        void FillAllLegalCandidates();
        void ClearAllCandidates();
        void ClearCandidates(Position position);

        IGrid Clone();
        void Restart();
        bool IsValueLegal(Position position);
    }
}
