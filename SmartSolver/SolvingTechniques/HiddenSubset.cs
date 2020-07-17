using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.SolvingTechniques
{
    public class HiddenSubset : ISolvingTechnique
    {
        public Position Position => Positions.First();

        public IEnumerable<Position> Positions { get; }

        public IEnumerable<InputValue> Values { get; }

        public House House { get; }

        public HiddenSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            Positions = positions;
            Values = values;
            House = Position.GetHouses(positions).First();
        }

        public bool CanExecute(IGrid grid)
        {
            return ValuesToRemove()
                .Any(value => Positions.Any(pos => grid.HasCandidate(pos, value)));
        }

        public void Execute(IGrid grid)
        {
            foreach( var value in ValuesToRemove() )
            {
                foreach( var pos in Positions )
                {
                    grid.RemoveCandidate(pos, value);
                }
            }
        }

        public IEnumerable<InputValue> ValuesToRemove()
        {
            return InputValue.NonEmpty.Except(Values);
            //foreach( var value in InputValue.NonEmpty )
            //{
            //    if( !Values.Contains(value) )
            //    {
            //        yield return value;
            //    }
            //}
        }
    }
}
