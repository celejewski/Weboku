using System.Collections.Generic;
using System.Linq;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class HiddenSubsetDisplayer : BaseSolvingTechniqueDisplayer
    {
        protected Position Position => _positions.First();
        protected readonly IEnumerable<Position> _positions;
        protected readonly IEnumerable<Value> _values;
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
            foreach (var value in _hiddenSubset.ValuesToRemove())
            {
                _displayer.MarkIfHasCandidate(Color.Illegal, _positions, value);
            }

            foreach (var value in _values)
            {
                _displayer.MarkIfHasCandidate(Color.Legal, _positions, value);
            }

            _displayer.HighlightHouse(Position, _house);
            _displayer.SetValueFilter(Value.None);
        }
    }
}