using Core.Data;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NakedSingle : BaseSolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _value;

        public NakedSingle(Position position, InputValue value)
            : base("naked-single")
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
            displayer.SetTitle(TitleKey);
            displayer.SetDescription(DescriptionKey, _position, _value, _position);
            displayer.Mark(Color.Legal, _position, _value);
            displayer.HighlightBlock(_position);
            displayer.HighlightCol(_position);
            displayer.HighlightRow(_position);
            displayer.SetValueFilter(_value);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.SetInput(_value, _position);
        }
    }
}
