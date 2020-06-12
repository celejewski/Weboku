using Core.Data;
using System;

namespace UI.BlazorWASM.Providers
{
    public interface ISudokuProvider
    {
        ICell[,] Cells { get; }

        void SetValue(int x, int y, int value);
        void ToggleCandidate(int x, int y, int value);

        void FillAllCandidates();
        void ClearAllCandidates();

        void RestartGame();

        IGrid GetGridClone();
        void AssignFrom(IGrid source);

        Sudoku Sudoku { get; set; }

        event Action OnCandidatesChanged;
        event Action OnValueChanged;
        event Action OnValueOrCandidatesChanged;
    }
}
