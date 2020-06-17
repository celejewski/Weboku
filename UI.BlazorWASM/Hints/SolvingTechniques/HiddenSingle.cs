using Core.Data;
using System;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class HiddenSingle :BaseSolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _inputValue;

        private House _house;
        private string _houseFormated;
        public HiddenSingle(Position position, InputValue inputValue)
            :base("Hidden Single")
        {
            _position = position;
            _inputValue = inputValue;

            _explanationSteps.AddRange( new Action<Displayer, Informer>[] { Explain1, Explain2, Explain3 } );
        }

        public override bool CanExecute(Informer informer)
        {
            return informer.HasCandidate(_position, _inputValue);
        }

        private void SetupDisplayer(Displayer displayer, Informer informer)
        {
            _house = HintsHelper.FindHouse(_position,
                positions => positions.Where(pos => informer.HasCandidate(pos, _inputValue))
                                      .Count() == 1
                );
            _houseFormated = Displayer.Format(_house, _position);

            displayer.SetTitle(_title);
            displayer.HighlightHouse(_position, _house);
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);

            displayer.SetDescription($"In {Displayer.Format(_house, _position)} value {_inputValue:d} can only be placed in cell {_position}");
            displayer.SetValueFilter(_inputValue);

            displayer.Mark(Enums.Color.Legal, _position, _inputValue);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.SetInput(_inputValue, _position);
        }

        private void Explain1(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            displayer.SetValueFilter(0);
            displayer.SetDescription($"Hidden single is when in row, column or block candidate can be only found in one cell. In this case lets focus on {_houseFormated}.");
        }

        private void Explain2(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription($"When we filter value {_inputValue:D} we can notice that in {_houseFormated} it can go only to {_position}. Still don't see it?");
        }
        private void Explain3(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            var posInHouse = HintsHelper.GetPositionsInHouse(_position, _house);
            displayer.MarkInputOrCandidate(Enums.Color.Illegal, posInHouse, _inputValue);
            displayer.MarkCells(Enums.Color.Illegal, posInHouse);
            displayer.Mark(Enums.Color.Legal, _position, _inputValue);
            displayer.SetDescription($"Other cells already have different values or don't have {_inputValue:D} as candidate. Only place where {_inputValue:D} can go in {_houseFormated} is cell {_position}.");
        }
    }
}
