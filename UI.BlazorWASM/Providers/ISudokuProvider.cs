using Core.Data;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace UI.BlazorWASM.Providers
{
    public interface ISudokuProvider
    {
        ICell[,] Cells { get; }

        void SetValue(int x, int y, int value);
        void ToggleCandidate(int x, int y, int value);
        void AssignFrom(IGrid source);

        void FillAllCandidates();
        void ClearAllCandidates();

        void Restart();

        IGrid GetGridClone();

        Sudoku Sudoku { get; set; }

        event Action OnCandidatesChanged;
        event Action OnValueChanged;
        event Action OnValueOrCandidatesChanged;
    }
}
