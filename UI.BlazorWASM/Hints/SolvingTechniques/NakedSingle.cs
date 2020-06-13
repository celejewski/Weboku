using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NakedSingle : ISolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _value;

        public NakedSingle(Position position, InputValue value)
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
            displayer.SetTitle("Naked Single");
            displayer.SetDescription($"There is only one value left in cell r{_position.Y+1}c{_position.X+1}.");
            displayer.Mark(Color.Legal, _position);
        }

        public void Execute(Executor executor)
        {
            executor.SetInput(_value, _position);
        }
    }
}
