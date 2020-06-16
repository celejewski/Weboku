using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedCandidatesPointing : BaseSolvingTechnique
    {
        private readonly int _block;
        private readonly InputValue _inputValue;
        private readonly IEnumerable<Position> _positionsToRemoveFrom;

        public LockedCandidatesPointing(int block, InputValue inputValue, IEnumerable<Position> positionToRemoveFrom)
            :base("Locked Candidates - Pointing")
        {
            _block = block;
            _inputValue = inputValue;
            _positionsToRemoveFrom = positionToRemoveFrom;
        }
        public override bool CanExecute(Informer informer)
        {
            return _positionsToRemoveFrom.Any(pos => informer.HasCandidate(pos, _inputValue));
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            var rowOrCol = HintsHelper.Format(RowOrCol(informer));
            displayer.SetTitle(_title);
            displayer.SetDescription(
                $"In block {_block+1}, value {_inputValue:D} can only be placed in one {rowOrCol}. " +
                $"This means that value {_inputValue:D} cannot occur in other blocks in this {rowOrCol}, " +
                $"because then there would be no option to put a value of {_inputValue:D} in block {_block+1}");

            var positionsWithCandidate = informer.GetPositionsWithCandidate(RowOrCol(informer), _positionsToRemoveFrom.First(), _inputValue);
            displayer.Mark(Enums.Color.Legal, positionsWithCandidate, _inputValue);

            var toMarkIllegal = informer.WithCandidate(_positionsToRemoveFrom, _inputValue);
            displayer.Mark(Enums.Color.Illegal, toMarkIllegal, _inputValue);
            displayer.HighlightBlock(_block);
            displayer.HighlightHouse(_positionsToRemoveFrom.First(), RowOrCol(informer));
            displayer.SetValueFilter(_inputValue);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.RemoveCandidates(_inputValue, _positionsToRemoveFrom);
        }

        private House RowOrCol(Informer informer)
        {
            var positionsInBlock = HintsHelper.GetPositionsInBlock(_block);
            var positionsWithCandidate = positionsInBlock.Where(pos => informer.HasCandidate(pos, _inputValue));
            return positionsWithCandidate.First().X == positionsWithCandidate.Last().X ? House.Col : House.Row;
        }
    }
}
