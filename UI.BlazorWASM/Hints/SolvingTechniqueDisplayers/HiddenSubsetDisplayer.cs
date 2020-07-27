using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class HiddenSubsetDisplayer : BaseSolvingTechniqueDisplayer
    {

        protected Position Position => _positions.First();
        protected readonly IEnumerable<Position> _positions;
        protected readonly IEnumerable<InputValue> _values;
        protected readonly House _house;
        private readonly HiddenSubset _hiddenSubset;

        public HiddenSubsetDisplayer(Informer informer, Displayer displayer, HiddenSubset hiddenSubset)
            : base(informer, displayer, hiddenSubset, "hidden-subset")
        {
            _positions = hiddenSubset.Positions;
            _values = hiddenSubset.Values;
            _house = hiddenSubset.House;
            _hiddenSubset = hiddenSubset;
        }


        public override void DisplaySolution()
        {
            _displayer.SetTitle(TitleKey);
            foreach( var value in _hiddenSubset.ValuesToRemove() )
            {
                _displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positions, value);
            }

            foreach( var value in _values )
            {
                _displayer.MarkIfHasCandidate(Enums.Color.Legal, _positions, value);
            }

            _displayer.HighlightHouse(Position, _house);
            _displayer.SetValueFilter(InputValue.None);
        }
    }
}
