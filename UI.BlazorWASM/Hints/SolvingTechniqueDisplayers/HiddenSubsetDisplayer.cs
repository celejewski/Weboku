using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class HiddenSubsetDisplayer : BaseDisplaySolvingTechnique
    {

        protected Position Position => _positions.First();
        protected readonly IEnumerable<Position> _positions;
        protected readonly IEnumerable<InputValue> _values;
        protected readonly House _house;
        private readonly HiddenSubset _hiddenSubset;

        public HiddenSubsetDisplayer(HiddenSubset hiddenSubset)
            : base(hiddenSubset, "hidden-subset")
        {
            _positions = hiddenSubset.Positions;
            _values = hiddenSubset.Values;
            _house = hiddenSubset.House;
            _hiddenSubset = hiddenSubset;
        }


        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            displayer.SetTitle(TitleKey);
            foreach( var value in _hiddenSubset.ValuesToRemove() )
            {
                displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positions, value);
            }

            foreach( var value in _values )
            {
                displayer.MarkIfHasCandidate(Enums.Color.Legal, _positions, value);
            }

            displayer.HighlightHouse(Position, _house);
            displayer.SetValueFilter(InputValue.Empty);
        }
    }
}
