using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedCandidatesClaiming : BaseSolvingTechnique
    {
        private readonly InputValue _inputValue;
        private readonly IEnumerable<Position> _positionsToRemoveCandidate;
        private readonly House _house;

        public LockedCandidatesClaiming(InputValue inputValue, IEnumerable<Position> positionsToRemoveCandidate, House house)
            :base("Locked Candidates - Claiming")
        {
            _inputValue = inputValue;
            _positionsToRemoveCandidate = positionsToRemoveCandidate;
            _house = house;
        }

        public override bool CanExecute(Informer informer)
        {
            return _positionsToRemoveCandidate.Any(pos => informer.HasCandidate(pos, _inputValue));
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            var block = _positionsToRemoveCandidate.First().Block;
            var house = Displayer.Format(_house);
            displayer.SetTitle(_title);
            displayer.SetDescription($"The only place in {house} where {_inputValue:D} can appear is in block {block+1}. " +
                $"So the value of {_inputValue:D} cannot appear anywhere else in the block of {block+1} because then the {house} would have no value of {_inputValue:D} anywhere.");

            var positionsWithCandidate = informer.GetPositionsWithCandidate(House.Block, _positionsToRemoveCandidate.First(), _inputValue);
            var positionsWithLegalCandidate = positionsWithCandidate.Except(_positionsToRemoveCandidate);

            displayer.Mark(Enums.Color.Legal, positionsWithCandidate, _inputValue);
            var toMarkIllegal = informer.WithCandidate(_positionsToRemoveCandidate, _inputValue);
            displayer.Mark(Enums.Color.Illegal, toMarkIllegal, _inputValue);
            displayer.HighlightBlock(positionsWithLegalCandidate.First());
            displayer.HighlightHouse(positionsWithLegalCandidate.First(), _house);
            displayer.SetValueFilter(_inputValue);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.RemoveCandidates(_inputValue, _positionsToRemoveCandidate);
        }
    }
}
