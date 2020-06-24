using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
            : base("locked-candiates-pointing")
        {
            _block = block;
            _blockFormated = $"block {_block + 1}";
            _inputValue = inputValue;
            _positionsToRemoveFrom = positionToRemoveFrom;

            _explanationSteps.AddRange(new Action<Displayer, Informer>[]
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
            displayer.SetTitle(TitleKey);
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

            displayer.SetDescription(DescriptionKey, _blockFormated, _inputValue, _rowOrColFormated, _inputValue, _rowOrColFormated, _blockFormated);
        }

        public void Explain1(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightBlock(_block);
            displayer.SetValueFilter(InputValue.Empty);
            displayer.SetDescription(ExplanationKey(1), _blockFormated);
        }

        public void Explain2(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightBlock(_block);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription(ExplanationKey(2), _inputValue);
        }

        public void Explain3(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);
            displayer.HighlightBlock(_block);
            displayer.SetValueFilter(_inputValue);
            displayer.Mark(Enums.Color.Legal, PositionWithLegalCandidates(informer), _inputValue);
            displayer.SetDescription(ExplanationKey(3), _inputValue);
        }
        public void Explain4(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);
            //displayer.HighlightBlock(_block);
            displayer.HighlightHouse(_positionsToRemoveFrom.First(), RowOrCol(informer));
            displayer.SetValueFilter(_inputValue);
            displayer.Mark(Enums.Color.Legal, PositionWithLegalCandidates(informer), _inputValue);
            displayer.SetDescription(ExplanationKey(4), _rowOrColFormated);
        }
        public void Explain5(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);
            displayer.HighlightBlock(_block);
            displayer.HighlightHouse(_positionsToRemoveFrom.First(), RowOrCol(informer));
            displayer.SetValueFilter(_inputValue);
            displayer.Mark(Enums.Color.Illegal, _positionsToRemoveFrom, _inputValue);
            displayer.Mark(Enums.Color.Legal, PositionWithLegalCandidates(informer), _inputValue);
            displayer.SetDescription(ExplanationKey(5), _inputValue, _rowOrColFormated);
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
            return positionsWithCandidate.First().x == positionsWithCandidate.Last().x ? House.Col : House.Row;
        }
    }
}
