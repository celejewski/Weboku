using Core.Data;
using System;

namespace UI.BlazorWASM.Providers
{
    public interface IGridProvider
    {
        /// <summary>
        /// Exposes underlying IGrid.
        /// </summary>
        public IGrid Grid { get; set; }

        /// <summary>
        /// Returns value of cell.
        /// </summary>
        InputValue GetValue(int x, int y);

        /// <summary>
        /// Sets value of cell and removes candidate from seen cells.
        /// </summary>
        void SetValue(int x, int y, InputValue value);

        /// <summary>
        /// Returns if candidate is in cell.
        /// </summary>
        bool HasCandidate(int x, int y, InputValue value);

        /// <summary>
        /// Adds candidate to cell.
        /// </summary>
        void AddCandidate(int x, int y, InputValue value);

        /// <summary>
        /// Removes candidate from cell.
        /// </summary>
        void RemoveCandidate(int x, int y, InputValue value);

        /// <summary>
        /// Toggles candidate in cell.
        /// </summary>
        void ToggleCandidate(int x, int y, InputValue value);

        /// <summary>
        /// Removes all candidates in grid.
        /// </summary>
        void ClearCandidates();

        /// <summary>
        /// Removes all candidates in cell.
        /// </summary>
        void ClearCandidates(int x, int y);

        /// <summary>
        /// Returns if the value is legal in the cell.
        /// </summary>
        bool IsValueLegal(int x, int y);

        /// <summary>
        /// Returns if the candidate is legal in the cell
        /// </summary>
        bool IsCandidateLegal(int x, int y, InputValue value);

        /// <summary>
        /// Return if cell is given.
        /// </summary>
        bool GetIsGiven(int x, int y);

        /// <summary>
        /// Returns count of candidates.
        /// </summary>
        int GetCandidatesCount(int x, int y);

        /// <summary>
        /// Fills all legal candidates.
        /// </summary>
        void FillAllLegalCandidates();

        /// <summary>
        /// Triggered when candidates are changed.
        /// </summary>
        event Action OnCandidatesChanged;

        /// <summary>
        /// Triggered when values are changed.
        /// </summary>
        event Action OnValueChanged;

        /// <summary>
        /// Triggered when values or candidates are changed.
        /// </summary>
        event Action OnValueOrCandidatesChanged;
    }
}
