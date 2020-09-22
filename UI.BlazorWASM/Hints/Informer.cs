using Core;
using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace UI.BlazorWASM.Hints
{
    /// <summary>
    /// Passes user readonly data to determine if ISolvingTechnique can execute.
    /// </summary>
    public class Informer
    {
        private readonly DomainFacade _domainFacade;

        public Informer(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public Value GetValue(Position position) => _domainFacade.GetInputValue(position);
        public bool HasValue(Position position) => _domainFacade.HasValue(position);
        public bool HasCandidate(Position position, Value value) => _domainFacade.HasCandidate(position, value);
        public Value GetSolution(Position position) => Value.None;

        public int GetCandidatesCount(Position position) => _domainFacade.GetCandidatesCount(position);

        public IEnumerable<Position> GetPositionsWithCandidate(House house, Position housePosition, Value inputValue)
        {
            return HintsHelper.GetPositionsInHouse(housePosition, house)
                .Where(pos => HasCandidate(pos, inputValue));
        }

        public IEnumerable<Position> WithCandidate(IEnumerable<Position> positions, Value inputValue)
        {
            return positions.Where(pos => HasCandidate(pos, inputValue));
        }
    }
}
