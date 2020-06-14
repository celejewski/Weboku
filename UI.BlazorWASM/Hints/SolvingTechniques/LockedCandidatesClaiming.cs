﻿using Core.Data;
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

            var positionsWithCandidate = informer.GetPositionsWithCandidate(House.Block, _positionsToRemoveCandidate.First(), _inputValue);
            var positionsWithLegalCandidate = positionsWithCandidate.Except(_positionsToRemoveCandidate);

            displayer.Mark(Enums.Color.Legal, positionsWithCandidate, _inputValue);
            var toMarkIllegal = informer.WithCandidate(_positionsToRemoveCandidate, _inputValue);
            displayer.Mark(Enums.Color.Illegal, toMarkIllegal, _inputValue);
            displayer.HighlightBlock(positionsWithLegalCandidate.First());
            displayer.HighlightHouse(positionsWithLegalCandidate.First(), _house);
            displayer.SetValueFilter(_inputValue);
        }

        public void Execute(Executor executor)
        {
            executor.RemoveCandidates(_inputValue, _positionsToRemoveCandidate);
        }
    }
}
