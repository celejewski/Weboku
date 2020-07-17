using Core.Data;
using SmartSolver.SolvingTechniques;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class NakedSingleDisplayer : BaseDisplaySolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _value;

        public NakedSingleDisplayer(NakedSingle nakedSingle) 
            : base(nakedSingle, "naked-single")
        {
            _position = nakedSingle.Position;
            _value = nakedSingle.Value;
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
    }
}
