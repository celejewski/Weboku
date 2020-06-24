namespace Core.Data
{
    public interface IGrid
    {
        bool GetIsGiven(int x, int y);
        void SetIsGiven(int x, int y, bool value);

        InputValue GetValue(int x, int y);
        void SetValue(int x, int y, InputValue value);
        bool HasValue(int x, int y);

        bool HasCandidate(int x, int y, InputValue value);
        int GetCandidatesCount(int x, int y);

        void AddCandidate(int x, int y, InputValue value);
        void RemoveCandidate(int x, int y, InputValue value);
        void ToggleCandidate(int x, int y, InputValue value);
        void FillCandidates();
        void ClearCandidates();
        void ClearCandidates(int x, int y);

        IGrid Clone();
    }
}
