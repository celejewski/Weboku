using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NakedSingle : BaseSolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _value;

        public NakedSingle(Position position, InputValue value)
            :base("Naked Single")
        {
            _position = position;
            _value = value;
        }

        public override bool CanExecute(Informer informer)
        {
            return informer.HasCandidate(_position, _value);
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            displayer.SetTitle(_title);
            displayer.SetDescription($"{_value:D} is the only candidate in cell {_position}");
            displayer.MarkCell(Color.Legal, _position);
            displayer.MarkCandidate(Color.Legal, _position, _value);
            displayer.HighlightBlock(_position);
            displayer.HighlightRow(_position);
            displayer.HighlightCol(_position);
            displayer.SetValueFilter(_value);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.SetInput(_value, _position);
        }
    }
}
