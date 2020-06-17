using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedCandidatesPointing : BaseSolvingTechnique
    {
        private readonly int _block;
        private readonly string _blockFormated;
        private string _rowOrColFormated;
        private readonly InputValue _inputValue;
        private readonly IEnumerable<Position> _positionsToRemoveFrom;

        public LockedCandidatesPointing(int block, InputValue inputValue, IEnumerable<Position> positionToRemoveFrom)
            :base("Locked Candidates - Pointing")
        {
            _block = block;
            _blockFormated = $"block {_block+1}";
            _inputValue = inputValue;
            _positionsToRemoveFrom = positionToRemoveFrom;

            _explanationSteps.AddRange( new Action<Displayer, Informer>[]
            {
                Explain1,
                Explain2,
                Explain3,
                Explain4,
                Explain5
            });
        }
        public override bool CanExecute(Informer informer)
        {
            return _positionsToRemoveFrom.Any(pos => informer.HasCandidate(pos, _inputValue));
        }

        private void SetupDisplay(Displayer displayer, Informer informer)
        {
            displayer.Clear();
            displayer.SetTitle(_title);
            _rowOrColFormated = Displayer.Format(RowOrCol(informer), _positionsToRemoveFrom.First());

        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);
            var positions = PositionsWithCandidate(informer);

            displayer.Mark(Enums.Color.Legal, positions, _inputValue);
            displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positionsToRemoveFrom, _inputValue);
            displayer.HighlightBlock(_block);
            displayer.HighlightHouse(_positionsToRemoveFrom.First(), RowOrCol(informer));
            displayer.SetValueFilter(_inputValue);

            displayer.SetDescription(
                $"In {_blockFormated} value {_inputValue:D} can be placed only in {_rowOrColFormated}, " +
                $"so any candidate {_inputValue:D} in {_rowOrColFormated} outside {_blockFormated} can be removed."
                );
        }

        public void Explain1(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightBlock(_block);
            displayer.SetValueFilter(InputValue.Empty);
            displayer.SetDescription($"Lets focus on {_blockFormated}...");
        }

        public void Explain2(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightBlock(_block);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription($"... with filter we can see where value {_inputValue:D} can be placed...");
        }

        public void Explain3(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);
            displayer.HighlightBlock(_block);
            displayer.SetValueFilter(_inputValue);
            displayer.Mark(Enums.Color.Legal, PositionWithLegalCandidates(informer), _inputValue);
            displayer.SetDescription($"... one of this cells has to be {_inputValue:D}, we don't know yet which one...");
        }
        public void Explain4(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);
            //displayer.HighlightBlock(_block);
            displayer.HighlightHouse(_positionsToRemoveFrom.First(), RowOrCol(informer));
            displayer.SetValueFilter(_inputValue);
            displayer.Mark(Enums.Color.Legal, PositionWithLegalCandidates(informer), _inputValue);
            displayer.SetDescription($"... but we can notice that all this cells lay in {_rowOrColFormated}...");
        }
        public void Explain5(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);
            displayer.HighlightBlock(_block);
            displayer.HighlightHouse(_positionsToRemoveFrom.First(), RowOrCol(informer));
            displayer.SetValueFilter(_inputValue);
            displayer.Mark(Enums.Color.Illegal, _positionsToRemoveFrom, _inputValue);
            displayer.Mark(Enums.Color.Legal, PositionWithLegalCandidates(informer), _inputValue);
            displayer.SetDescription($"... it means we can remove candidate {_inputValue:D} from other cells in {_rowOrColFormated}.");
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.RemoveCandidates(_inputValue, _positionsToRemoveFrom);
        }

        public IEnumerable<Position> PositionsWithCandidate(Informer informer)
        {
            return informer.GetPositionsWithCandidate(RowOrCol(informer), _positionsToRemoveFrom.First(), _inputValue);
        }

        public IEnumerable<Position> PositionWithLegalCandidates(Informer informer)
        {
            return PositionsWithCandidate(informer).Except(_positionsToRemoveFrom);
        }

        private House RowOrCol(Informer informer)
        {
            var positionsInBlock = HintsHelper.GetPositionsInBlock(_block);
            var positionsWithCandidate = positionsInBlock.Where(pos => informer.HasCandidate(pos, _inputValue));
            return positionsWithCandidate.First().X == positionsWithCandidate.Last().X ? House.Col : House.Row;
        }
    }
}
