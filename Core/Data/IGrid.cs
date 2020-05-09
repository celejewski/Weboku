namespace Core.Data
{
    public interface IGrid
    {
        ICell[,] Cells { get; }

        void AssignFrom(IGrid source);
        object Clone();
        void FillAllCandidates();
        bool IsLegalValue(int x, int y, int value);
        void SetGiven(int x, int y, int value);
        void SetValue(int x, int y, int value);
        void ToggleCandidate(int x, int y, int value);
    }
}