using Core.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace UI.BlazorWASM.Providers
{
    public interface ISudokuProvider : IProvider
    {
        ICell[,] Cells { get; }

        void SetValue(int x, int y, int value);
        void ToggleCandidate(int x, int y, int value);
        void AssignFrom(IGrid source);

        void FillAllCandidates();
        void ClearCandidates();

        void Restart();

        IGrid GetGridClone();
    }
}
