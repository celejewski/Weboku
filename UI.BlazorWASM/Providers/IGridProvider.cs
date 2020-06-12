using Core.Data;

namespace UI.BlazorWASM.Providers
{
    public interface IGridProvider
    {
        InputValue GetValue(int x, int y);
        void SetValue(int x, int y, InputValue value);

        bool HasCandidate(int x, int y, InputValue value);
        void AddCandidate(int x, int y, InputValue value);
        void RemoveCandidate(int x, int y, InputValue value);
        void ToggleCandidate(int x, int y, InputValue value);
        void FillCandidates();
        void ClearCandidates();
        void ClearCandidates(int x, int y);

        bool IsValueLegal(int x, int y);
        bool IsCandidateLegal(int x, int y, InputValue value);

        bool IsGiven(int x, int y);

        int GetCandidatesCount(int x, int y);
    }
}
