using System;
using System.Collections.Generic;
using System.Linq;
using Weboku.Application;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class LockedCandidatesClaimingDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Value _value;
        private readonly IEnumerable<Position> _positionsToRemoveCandidate;
        private IEnumerable<Position> _positionsWithLegalCandidate;
        private IEnumerable<Position> _positionsWithIllegalCandidate;
        private Position Position => _positionsWithLegalCandidate.First();
        private readonly House _house;
        private string _houseFormatted;
        private string _blockFormatted;

        public LockedCandidatesClaimingDisplayer(DomainFacade displayer, LockedCandidatesClaiming lockedCandidatesClaiming)
            : base(displayer, lockedCandidatesClaiming, "locked-candiates-claiming")
        {
            _value = lockedCandidatesClaiming.Value;
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
            var positionsWithCandidate = _informer.GetPositionsWithCandidate(House.Block, _positionsToRemoveCandidate.First(), _value);
            _positionsWithLegalCandidate = positionsWithCandidate.Except(_positionsToRemoveCandidate);
            _houseFormatted = _displayer.Format(_house, Position);
            _blockFormatted = _displayer.Format(House.Block, Position);
            _positionsWithIllegalCandidate = GetPositiosnWithIllegalCandidate(_informer);
        }

        public override void DisplaySolution()
        {
            SetupDisplay();

            _displayer.Mark(Color.Legal, _positionsWithLegalCandidate, _value);
            _displayer.Mark(Color.Illegal, _positionsWithIllegalCandidate, _value);
            _displayer.HighlightBlock(Position);
            _displayer.HighlightHouse(Position, _house);
            _displayer.SetValueFilter(_value);

            _displayer.SetDescription(DescriptionKey, _houseFormatted, _value, _blockFormatted, _value, _blockFormatted, _houseFormatted);
        }

        private void Explain1()
        {
            SetupDisplay();

            _displayer.HighlightHouse(Position, _house);
            _displayer.SetValueFilter(Value.None);
            _displayer.SetDescription(ExplanationKey(1), _houseFormatted);
        }

        private void Explain2()
        {
            SetupDisplay();

            _displayer.HighlightHouse(Position, _house);
            _displayer.SetValueFilter(_value);
            _displayer.SetDescription(ExplanationKey(2), _value);
        }

        private void Explain3()
        {
            SetupDisplay();

            _displayer.HighlightHouse(Position, _house);
            _displayer.MarkIfHasCandidate(Color.Legal, _positionsWithLegalCandidate, _value);
            _displayer.SetValueFilter(_value);
            _displayer.SetDescription(ExplanationKey(3), _value);
        }

        private void Explain4()
        {
            SetupDisplay();

            _displayer.HighlightBlock(Position);
            _displayer.MarkIfHasCandidate(Color.Legal, _positionsWithLegalCandidate, _value);
            _displayer.SetValueFilter(_value);
            _displayer.SetDescription(ExplanationKey(4), _blockFormatted);
        }

        private void Explain5()
        {
            SetupDisplay();

            _displayer.HighlightHouse(Position, _house);
            _displayer.HighlightBlock(Position);
            _displayer.MarkIfHasCandidate(Color.Legal, _positionsWithLegalCandidate, _value);
            _displayer.MarkIfHasCandidate(Color.Illegal, _positionsWithIllegalCandidate, _value);
            _displayer.SetValueFilter(_value);
            _displayer.SetDescription(ExplanationKey(5), _value, _blockFormatted);
        }

        public IEnumerable<Position> GetPositiosnWithIllegalCandidate(DomainFacade _informer)
        {
            return _informer.WithCandidate(_positionsToRemoveCandidate, _value);
        }
    }
}