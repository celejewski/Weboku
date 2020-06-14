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

        public void Display(Displayer displayer, Informer informer)
        {
            displayer.SetTitle("Naked Single");
            displayer.SetDescription($"There is only one valid candidate left in cell {_position}");
            displayer.MarkCell(Color.Legal, _position);
            displayer.MarkCandidate(Color.Legal, _position, _value);
            displayer.HighlightBlock(_position);
            displayer.HighlightRow(_position);
            displayer.HighlightCol(_position);
            displayer.SetValueFilter(_value);
        }

        public void Execute(Executor executor)
        {
            executor.SetInput(_value, _position);
        }
    }
}
