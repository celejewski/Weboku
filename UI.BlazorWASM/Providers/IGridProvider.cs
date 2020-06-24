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
        InputValue GetValue(Position pos);

        /// <summary>
        /// Sets value of cell and removes candidate from seen cells.
        /// </summary>
        void SetValue(Position pos, InputValue value);

        /// <summary>
        /// Returns true if has not empty value.
        /// </summary>
        bool HasValue(Position pos);

        /// <summary>
        /// Returns true if candidate is in cell.
        /// </summary>
        bool HasCandidate(Position pos, InputValue value);

        /// <summary>
        /// Adds candidate to cell.
        /// </summary>
        void AddCandidate(Position pos, InputValue value);

        /// <summary>
        /// Removes candidate from cell.
        /// </summary>
        void RemoveCandidate(Position pos, InputValue value);

        /// <summary>
        /// Toggles candidate in cell.
        /// </summary>
        void ToggleCandidate(Position pos, InputValue value);

        /// <summary>
        /// Removes all candidates in grid.
        /// </summary>
        void ClearCandidates();

        /// <summary>
        /// Removes all candidates in cell.
        /// </summary>
        void ClearCandidates(Position pos);

        /// <summary>
        /// Returns true if the value in the cell is legal.
        /// </summary>
        bool IsValueLegal(Position pos);

        /// <summary>
        /// Returns true if the candidate in the cell is legal.
        /// </summary>
        bool IsCandidateLegal(Position pos, InputValue value);

        /// <summary>
        /// Return true if cell is given.
        /// </summary>
        bool GetIsGiven(Position pos);

        /// <summary>
        /// Returns count of candidates in cell.
        /// </summary>
        int CandidatesCount(Position pos);

        /// <summary>
        /// Fills cells without value with all legal candidates.
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
