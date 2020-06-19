using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

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
            : base("Locked Candidates - Claiming")
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
            displayer.SetTitle(_title);
            var positionsWithCandidate = informer.GetPositionsWithCandidate(House.Block, _positionsToRemoveCandidate.First(), _inputValue);
            _positionsWithLegalCandidate = positionsWithCandidate.Except(_positionsToRemoveCandidate);
            _houseFormatted = Displayer.Format(_house, Position);
            _blockFormatted = $"block { Position.Block + 1 }";
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

            displayer.SetDescription(
                $"In {_houseFormatted} value {_inputValue:D} can be placed only in {_blockFormatted}, "
                + $"so any candidate {_inputValue:D} in {_blockFormatted} outside {_houseFormatted} can be removed.");
        }

        private void Explain1(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightHouse(Position, _house);
            displayer.SetValueFilter(InputValue.Empty);
            displayer.SetDescription($"Lets focus on {_houseFormatted}...");
        }
        private void Explain2(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightHouse(Position, _house);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription($"... with filter we can see where value {_inputValue:D} can be placed...");
        }
        private void Explain3(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightHouse(Position, _house);
            displayer.MarkIfHasCandidate(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription($"... one of this cells has to be {_inputValue:D}, we don't know yet which one...");
        }
        private void Explain4(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightBlock(Position);
            displayer.MarkIfHasCandidate(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription($"... but we can notice that all this cells lay in {_blockFormatted}...");
        }
        private void Explain5(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.HighlightHouse(Position, _house);
            displayer.HighlightBlock(Position);
            displayer.MarkIfHasCandidate(Enums.Color.Legal, _positionsWithLegalCandidate, _inputValue);
            displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positionsWithIllegalCandidate, _inputValue);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription($"... it means we can remove candidate {_inputValue:D} from other cells in {_blockFormatted}.");
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
