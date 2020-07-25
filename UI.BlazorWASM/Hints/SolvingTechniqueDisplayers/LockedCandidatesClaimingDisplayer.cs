using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class LockedCandidatesClaimingDisplayer : BaseSolvingTechniqueDisplayer
    {

        private readonly InputValue _inputValue;
        private readonly IEnumerable<Position> _positionsToRemoveCandidate;
        private IEnumerable<Position> _positionsWithLegalCandidate;
        private IEnumerable<Position> _positionsWithIllegalCandidate;
        private Position Position => _positionsWithLegalCandidate.First();
        private readonly House _house;
        private string _houseFormatted;
        private string _blockFormatted;

        public LockedCandidatesClaimingDisplayer(Informer informer, Displayer displayer, LockedCandidatesClaiming lockedCandidatesClaiming)
            : base(informer, displayer, lockedCandidatesClaiming, "locked-candiates-claiming")
        {
            _inputValue = lockedCandidatesClaiming.InputValue;
            _positionsToRemoveCandidate = lockedCandidatesClaiming.PositionsToRemoveCandidate;
            _house = lockedCandidatesClaiming.House;

            _explanationSteps.AddRange(new Action[]
            {
                Explain1,
                Explain2,
                Explain3,
                Explain4,
                Explain5,
            }
                );
        }

        private void SetupDisplay()
        {
            _displayer.Clear();
            _displayer.SetTitle(TitleKey);
            var positionsWithCandidate = _informer.GetPositionsWithCandidate(House.Block, _positionsToRemoveCandidate.First(), _inputValue);
            _positionsWithLegalCandidate = positionsWithCandidate.Except(_positionsToRemoveCandidate);
            _houseFormatted = _displayer.Format(_house, Position);
            _blockFormatted = _displayer.Format(House.Block, Position);
            _positionsWithIllegalCandidate = GetPositiosnWithIllegalCandidate(_informer);
        }

        public override void DisplaySolution()
        {
            SetupDisplay();

            _displayer.Mark(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            _displayer.Mark(Enums.Color.Illegal, _positionsWithIllegalCandidate, _inputValue);
            _displayer.HighlightBlock(Position);
            _displayer.HighlightHouse(Position, _house);
            _displayer.SetValueFilter(_inputValue);

            _displayer.SetDescription(DescriptionKey, _houseFormatted, _inputValue, _blockFormatted, _inputValue, _blockFormatted, _houseFormatted);
        }

        private void Explain1()
        {
            SetupDisplay();

            _displayer.HighlightHouse(Position, _house);
            _displayer.SetValueFilter(InputValue.Empty);
            _displayer.SetDescription(ExplanationKey(1), _houseFormatted);
        }
        private void Explain2()
        {
            SetupDisplay();

            _displayer.HighlightHouse(Position, _house);
            _displayer.SetValueFilter(_inputValue);
            _displayer.SetDescription(ExplanationKey(2), _inputValue);
        }
        private void Explain3()
        {
            SetupDisplay();

            _displayer.HighlightHouse(Position, _house);
            _displayer.MarkIfHasCandidate(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            _displayer.SetValueFilter(_inputValue);
            _displayer.SetDescription(ExplanationKey(3), _inputValue);
        }
        private void Explain4()
        {
            SetupDisplay();

            _displayer.HighlightBlock(Position);
            _displayer.MarkIfHasCandidate(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            _displayer.SetValueFilter(_inputValue);
            _displayer.SetDescription(ExplanationKey(4), _blockFormatted);
        }
        private void Explain5()
        {
            SetupDisplay();

            _displayer.HighlightHouse(Position, _house);
            _displayer.HighlightBlock(Position);
            _displayer.MarkIfHasCandidate(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            _displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positionsWithIllegalCandidate, _inputValue);
            _displayer.SetValueFilter(_inputValue);
            _displayer.SetDescription(ExplanationKey(5), _inputValue, _blockFormatted);
        }

        public IEnumerable<Position> GetPositiosnWithIllegalCandidate(Informer _informer)
        {
            return _informer.WithCandidate(_positionsToRemoveCandidate, _inputValue);
        }
    }
}
