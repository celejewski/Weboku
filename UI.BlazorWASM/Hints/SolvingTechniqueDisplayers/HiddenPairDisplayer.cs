using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class HiddenPairDisplayer : HiddenSubsetDisplayer
    {
        public HiddenPairDisplayer(Informer informer, Displayer displayer, HiddenPair hiddenSubset) 
            : base(informer, displayer, hiddenSubset)
        {
            _locKey = "hidden-pair";

            var positions = hiddenSubset.Positions;
            var values = hiddenSubset.Values;

            _pos1 = positions.ElementAt(0);
            _pos2 = positions.ElementAt(1);
            _value1 = values.ElementAt(0);
            _value2 = values.ElementAt(1);
        }

        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value1;
        private readonly InputValue _value2;

        public override void DisplaySolution()
        {
            base.DisplaySolution();

            var houseFormatted = _displayer.Format(_house, Position);
            _displayer.SetDescription("hidden-pair__description",
                houseFormatted, _value1, _value2, _pos1, _pos2);
        }
    }
}
