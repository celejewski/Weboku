using Core.Data;
using System;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    /// <summary>
    /// Passes user readonly data to determine if ISolvingTechnique can execute.
    /// </summary>
    public class Informer
    {
        private readonly IGridProvider _gridProvider;
        private readonly SudokuProvider _sudokuProvider;

        public Informer(IGridProvider gridProvider, SudokuProvider sudokuProvider)
        {
            _gridProvider = gridProvider;
            _sudokuProvider = sudokuProvider;
        }

        public bool HasValue(Position position) => _gridProvider.HasValue(position.X, position.Y);
        public bool HasCandidate(Position position, InputValue value) => _gridProvider.HasCandidate(position.X, position.Y, value);
        public InputValue GetSolution(Position position) => _sudokuProvider.GetSolution(position.X, position.Y);
    }
}
