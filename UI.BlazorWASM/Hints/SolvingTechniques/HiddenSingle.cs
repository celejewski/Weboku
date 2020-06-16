using Core.Data;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class HiddenSingle :ISolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _inputValue;

        public HiddenSingle(Position position, InputValue inputValue)
        {
            _position = position;
            _inputValue = inputValue;
        }

        public bool CanExecute(Informer informer)
        {
            return informer.HasCandidate(_position, _inputValue);
        }

        public void Display(Displayer displayer, Informer informer)
        {

            var house = HintsHelper.FindHouse(_position,
                positions => positions.Where(pos => informer.HasCandidate(pos, _inputValue))
                                      .Count() == 1
                );

            displayer.SetTitle("Hidden Single");
            displayer.SetDescription($"In {HintsHelper.Format(house, _position)} value {_inputValue:d} can only be placed in cell {_position}");

            var posInHouse = HintsHelper.GetPositionsInHouse(_position, house);
            displayer.MarkInputOrCandidate(Enums.Color.Illegal, posInHouse, _inputValue);
            displayer.MarkCells(Enums.Color.Illegal, posInHouse);
            displayer.MarkCell(Enums.Color.Legal, _position);
            displayer.MarkCandidate(Enums.Color.Legal, _position, _inputValue);
            displayer.HighlightHouse(_position, house);
            displayer.SetValueFilter(_inputValue);
        }

        public void Execute(Executor executor, Informer informer)
        {
            executor.SetInput(_inputValue, _position);
        }
    }
}
