using Core.Data;
using System;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FullHouse : ISolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _value;

        public FullHouse(Position position, InputValue value)
        {
            _position = position;
            _value = value;
        }

        public bool CanExecute(Informer informer)
        {
            return informer.HasCandidate(_position, _value);
        }

        public void Display(Displayer displayer, Informer informer)
        {

            var house = HintsHelper.FindHouse(_position,
                positions => positions.Where(pos => !informer.HasValue(pos))
                                      .Count() == 1
                );

            displayer.SetTitle("Full House");
            displayer.SetDescription($"Cell {_position} is only one without value in this house.");
            displayer.MarkCell(Enums.Color.Legal, _position);
            displayer.MarkCandidate(Enums.Color.Legal, _position, _value);
            displayer.HighlightHouse(_position, house);
            displayer.SetValueFilter(_value);
        }

        public void Execute(Executor executor)
        {
            executor.SetInput(_value, _position);
        }
    }
}
