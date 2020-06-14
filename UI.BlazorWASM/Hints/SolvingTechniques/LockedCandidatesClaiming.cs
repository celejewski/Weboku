using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedCandidatesClaiming : ISolvingTechnique
    {
        private readonly InputValue _inputValue;
        private readonly IEnumerable<Position> _positionsToRemoveCandidate;
        private readonly House _house;

        public LockedCandidatesClaiming(InputValue inputValue, IEnumerable<Position> positionsToRemoveCandidate, House house)
        {
            _inputValue = inputValue;
            _positionsToRemoveCandidate = positionsToRemoveCandidate;
            _house = house;
        }

        public bool CanExecute(Informer informer)
        {
            return _positionsToRemoveCandidate.Any(pos => informer.HasCandidate(pos, _inputValue));
        }

        public void Display(Displayer displayer, Informer informer)
        {
            displayer.SetTitle("Locked Candidates - Claiming");
            displayer.SetDescription("Claiming");

            var positionsInBlock = HintsHelper.GetPositionsInBlock(_positionsToRemoveCandidate.First());
            var positionsWithCandidate = positionsInBlock.Where(pos => informer.HasCandidate(pos, _inputValue));
            var positionsWithLegalCandidate = positionsWithCandidate.Except(_positionsToRemoveCandidate);

            displayer.MarkCells(Enums.Color.Legal, positionsWithCandidate);
            displayer.MarkCandidates(Enums.Color.Legal, positionsWithCandidate, _inputValue);

            displayer.MarkCells(Enums.Color.Illegal, _positionsToRemoveCandidate);
            displayer.MarkCandidates(Enums.Color.Illegal, _positionsToRemoveCandidate, _inputValue);
            displayer.HighlightBlock(positionsWithLegalCandidate.First());
            displayer.HighlightHouse(positionsWithLegalCandidate.First(), _house);
            displayer.SetValueFilter(_inputValue);
        }

        public void Execute(Executor executor)
        {
            executor.RemoveCandidate(_inputValue, _positionsToRemoveCandidate);
        }
    }
}
