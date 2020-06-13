using Core.Data;
using System;
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

        public void Display(Displayer displayer)
        {
            displayer.SetTitle("Full House");
            displayer.SetDescription("This cell is only without value in house.");
            displayer.Mark(Enums.Color.Legal, _position);
        }

        public void Execute(Executor executor)
        {
            executor.SetInput(_value, _position);
        }
    }
}
