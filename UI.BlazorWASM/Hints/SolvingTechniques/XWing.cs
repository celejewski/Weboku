using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class XWing : BaseSolvingTechnique
    {
        private readonly InputValue _value;
        private readonly IEnumerable<Position> _positions;
        private readonly IEnumerable<Position> _positionsToRemove;
        private readonly House _house;

        public XWing(InputValue value, IEnumerable<Position> positions, IEnumerable<Position> positionsToRemove, House house)
            : base("xwing")
        {
            _value = value;
            _positions = positions;
            _positionsToRemove = positionsToRemove;
            _house = house;
        }
        public override bool CanExecute(Informer informer)
        {
            return _positionsToRemove.Any(pos => informer.HasCandidate(pos, _value));
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);

            var opositeHouse = _house == House.Col ? House.Row : House.Col;
            displayer.SetDescription(
                DescriptionKey, 
                displayer.Format(_house, _positions.First()), 
                displayer.Format(_house, _positions.Last()),
                _value,
                displayer.Format(opositeHouse, _positions.First()),
                displayer.Format(opositeHouse, _positions.Last())
                );
            displayer.SetValueFilter(_value);

            foreach( var pos in _positions )
            {
                displayer.HighlightCol(pos);
                displayer.HighlightRow(pos);
            }

            System.Console.WriteLine(_positions.ElementAt(0));
            displayer.MarkCandidates(Enums.Color.Legal, _positions, _value);
            displayer.MarkCandidates(Enums.Color.Illegal, _positionsToRemove, _value);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.RemoveCandidates(_value, _positionsToRemove);
        }
    }
}
