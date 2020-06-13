using Core.Data;
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

        public void Display(Displayer displayer)
        {
            displayer.SetTitle("Hidden Single");
            displayer.SetDescription("This cell is only option to place this candidate.");
            displayer.Mark(Enums.Color.Legal, _position);
        }

        public void Execute(Executor executor)
        {
            executor.SetInput(_inputValue, _position);
        }
    }
}
