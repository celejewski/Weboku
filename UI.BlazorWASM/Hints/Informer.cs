using Core.Data;
using System.Collections.Generic;
using System.Linq;
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

        public InputValue GetValue(Position position) => _gridProvider.GetValue(position);
        public bool HasValue(Position position) => _gridProvider.HasValue(position);
        public bool HasCandidate(Position position, InputValue value) => _gridProvider.HasCandidate(position, value);
        public InputValue GetSolution(Position position) => _sudokuProvider.GetSolution(position.x, position.y);

        public int GetCandidatesCount(Position position) => _gridProvider.CandidatesCount(position);

        public IEnumerable<Position> GetPositionsWithCandidate(House house, Position housePosition, InputValue inputValue)
        {
            return HintsHelper.GetPositionsInHouse(housePosition, house)
                .Where(pos => HasCandidate(pos, inputValue));
        }

        public IEnumerable<Position> WithCandidate(IEnumerable<Position> positions, InputValue inputValue)
        {
            return positions.Where(pos => HasCandidate(pos, inputValue));
        }
    }
}
