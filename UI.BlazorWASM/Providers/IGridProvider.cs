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
        void RemoveCandidates(int x, int y);
        void ToggleCandidate(int x, int y, InputValue value);
        void FillCandidates();
        void ClearCandidates();

        bool IsValueLegal(int x, int y);
        bool IsCandidateLegal(int x, int y, InputValue value);

        bool IsGiven(int x, int y);
    }
}
