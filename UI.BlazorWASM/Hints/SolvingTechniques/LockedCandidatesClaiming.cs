﻿using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedCandidatesClaiming : BaseSolvingTechnique
    {
        private readonly InputValue _inputValue;
        private readonly IEnumerable<Position> _positionsToRemoveCandidate;
        private IEnumerable<Position> _positionsWithLegalCandidate;
        private IEnumerable<Position> _positionsWithIllegalCandidate;
        private Position Position => _positionsWithLegalCandidate.First();
        private readonly House _house;
        private string _houseFormatted;
        private string _blockFormatted;

        public LockedCandidatesClaiming(InputValue inputValue, IEnumerable<Position> positionsToRemoveCandidate, House house)
            : base("locked-candiates-claiming")
        {
            _inputValue = inputValue;
            _positionsToRemoveCandidate = positionsToRemoveCandidate;
            _house = house;

            _explanationSteps.AddRange(new Action<Displayer, Informer>[]
            {
                Explain1,
                Explain2,
                Explain3,
                Explain4,
                Explain5,
            }
                );
        }

        public override bool CanExecute(Informer informer)
        {
            return _positionsToRemoveCandidate.Any(pos => informer.HasCandidate(pos, _inputValue));
        }

        private void SetupDisplay(Displayer displayer, Informer informer)
        {
            displayer.Clear();
            displayer.SetTitle(TitleKey);
            var positionsWithCandidate = informer.GetPositionsWithCandidate(House.Block, _positionsToRemoveCandidate.First(), _inputValue);
            _positionsWithLegalCandidate = positionsWithCandidate.Except(_positionsToRemoveCandidate);
            _houseFormatted = displayer.Format(_house, Position);
            _blockFormatted = displayer.Format(House.Block, Position);
            _positionsWithIllegalCandidate = GetPositiosnWithIllegalCandidate(informer);
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.Mark(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            displayer.Mark(Enums.Color.Illegal, _positionsWithIllegalCandidate, _inputValue);
            displayer.HighlightBlock(Position);
            displayer.HighlightHouse(Position, _house);
            displayer.SetValueFilter(_inputValue);

            displayer.SetDescription(DescriptionKey, _houseFormatted, _inputValue, _blockFormatted, _inputValue, _blockFormatted, _houseFormatted);
        }

        private void Explain1(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightHouse(Position, _house);
            displayer.SetValueFilter(InputValue.Empty);
            displayer.SetDescription(ExplanationKey(1), _houseFormatted);
        }
        private void Explain2(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightHouse(Position, _house);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription(ExplanationKey(2), _inputValue);
        }
        private void Explain3(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightHouse(Position, _house);
            displayer.MarkIfHasCandidate(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription(ExplanationKey(3), _inputValue);
        }
        private void Explain4(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightBlock(Position);
            displayer.MarkIfHasCandidate(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription(ExplanationKey(4), _blockFormatted);
        }
        private void Explain5(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightHouse(Position, _house);
            displayer.HighlightBlock(Position);
            displayer.MarkIfHasCandidate(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positionsWithIllegalCandidate, _inputValue);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription(ExplanationKey(5), _inputValue, _blockFormatted);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.RemoveCandidates(_inputValue, _positionsToRemoveCandidate);
        }

        public IEnumerable<Position> GetPositiosnWithIllegalCandidate(Informer informer)
        {
            return informer.WithCandidate(_positionsToRemoveCandidate, _inputValue);
        }
    }
}
