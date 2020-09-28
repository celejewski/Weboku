using Core.Data;
using Core.Hints.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class LockedCandidatesPointingDisplayer : BaseSolvingTechniqueDisplayer
    {

        private readonly int _block;
        private string _rowOrColFormated;
        private readonly Value _value;
        private readonly IEnumerable<Position> _positionsToRemoveFrom;

        public LockedCandidatesPointingDisplayer(Informer informer, Displayer displayer, LockedCandidatesPointing lockedCandidatesPointing)
            : base(informer, displayer, lockedCandidatesPointing, "locked-candiates-pointing")
        {
            _block = lockedCandidatesPointing.Block;
            _value = lockedCandidatesPointing.Value;
            _positionsToRemoveFrom = lockedCandidatesPointing.PositionsToRemoveFrom;

            _explanationSteps.AddRange(new Action[]
            {
                Explain1,
                Explain2,
                Explain3,
                Explain4,
                Explain5
            });
        }

        private void SetupDisplay()
        {
            _displayer.Clear();
            _displayer.SetTitle(TitleKey);
            _rowOrColFormated = _displayer.Format(RowOrCol(_informer), _positionsToRemoveFrom.First());

        }

        public override void DisplaySolution()
        {
            SetupDisplay();
            var positions = PositionsWithCandidate(_informer);

            _displayer.Mark(Enums.Color.Legal, positions, _value);
            _displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positionsToRemoveFrom, _value);
            _displayer.HighlightBlock(_block);
            _displayer.HighlightHouse(_positionsToRemoveFrom.First(), RowOrCol(_informer));
            _displayer.SetValueFilter(_value);

            var blockFormatted = _displayer.Format(House.Block, Position.FromBlock(_block));
            _displayer.SetDescription(DescriptionKey, blockFormatted, _value, _rowOrColFormated, _value, _rowOrColFormated, blockFormatted);
        }

        public void Explain1()
        {
            SetupDisplay();

            _displayer.HighlightBlock(_block);
            _displayer.SetValueFilter(Value.None);
            var blockFormatted = _displayer.Format(House.Block, Position.FromBlock(_block));
            _displayer.SetDescription(ExplanationKey(1), blockFormatted);
        }

        public void Explain2()
        {
            SetupDisplay();

            _displayer.HighlightBlock(_block);
            _displayer.SetValueFilter(_value);
            _displayer.SetDescription(ExplanationKey(2), _value);
        }

        public void Explain3()
        {
            SetupDisplay();
            _displayer.HighlightBlock(_block);
            _displayer.SetValueFilter(_value);
            _displayer.Mark(Enums.Color.Legal, PositionWithLegalCandidates(_informer), _value);
            _displayer.SetDescription(ExplanationKey(3), _value);
        }
        public void Explain4()
        {
            SetupDisplay();
            //_displayer.HighlightBlock(_block);
            _displayer.HighlightHouse(_positionsToRemoveFrom.First(), RowOrCol(_informer));
            _displayer.SetValueFilter(_value);
            _displayer.Mark(Enums.Color.Legal, PositionWithLegalCandidates(_informer), _value);
            _displayer.SetDescription(ExplanationKey(4), _rowOrColFormated);
        }
        public void Explain5()
        {
            SetupDisplay();
            _displayer.HighlightBlock(_block);
            _displayer.HighlightHouse(_positionsToRemoveFrom.First(), RowOrCol(_informer));
            _displayer.SetValueFilter(_value);
            _displayer.Mark(Enums.Color.Illegal, _positionsToRemoveFrom, _value);
            _displayer.Mark(Enums.Color.Legal, PositionWithLegalCandidates(_informer), _value);
            _displayer.SetDescription(ExplanationKey(5), _value, _rowOrColFormated);
        }

        public IEnumerable<Position> PositionsWithCandidate(Informer _informer)
        {
            return _informer.GetPositionsWithCandidate(RowOrCol(_informer), _positionsToRemoveFrom.First(), _value);
        }

        public IEnumerable<Position> PositionWithLegalCandidates(Informer _informer)
        {
            return PositionsWithCandidate(_informer).Except(_positionsToRemoveFrom);
        }

        private House RowOrCol(Informer _informer)
        {
            var positionsInBlock = Position.Blocks[_block];
            var positionsWithCandidate = positionsInBlock.Where(pos => _informer.HasCandidate(pos, _value));
            return positionsWithCandidate.First().x == positionsWithCandidate.Last().x ? House.Col : House.Row;
        }
    }
}
