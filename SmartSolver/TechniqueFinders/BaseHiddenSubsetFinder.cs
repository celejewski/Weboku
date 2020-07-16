using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public abstract class BaseHiddenSubsetFinder : BaseTechniqueFinder
    {
        protected IEnumerable<(IEnumerable<Position> positions, IEnumerable<InputValue> values)> HiddenSubset(IGrid input, int depth)
        {
            foreach( var house in Position.Houses )
            {
                var result = HiddenSubsetStep(input, house, new HashSet<Position>(), new List<InputValue>(), depth);
                if( result != default ) return result;
            }
            return default;
        }

        private IEnumerable<(IEnumerable<Position> positions, IEnumerable<InputValue> values)> HiddenSubsetStep(
            IGrid input,
            IEnumerable<Position> house,
            HashSet<Position> positions,
            List<InputValue> values,
            int depth)
        {
            if( positions.Count > depth ) yield break;
            if( values.Count == depth )
            {
                var isAnyCandidateAnywhereToRemove = positions.Any(
                    pos => InputValue.NonEmpty.Except(values).Any(value => input.HasCandidate(pos, value)));
                if( isAnyCandidateAnywhereToRemove ) yield return (positions, values);
                else yield break;
            }

            foreach( var value in InputValue.NonEmpty.Except(values) )
            {
                var positionsWithCandidate = house.Where(pos => input.HasCandidate(pos, value));
                var count = positionsWithCandidate.Count();
                if( count > depth || count == 0 ) continue;

                var positionsNew = positions.ToHashSet();
                positionsNew.UnionWith(positionsWithCandidate);
                var valuesNew = new List<InputValue>(values) { value };
                var result = HiddenSubsetStep(input, house, positionsNew, valuesNew, depth);
                if( result != default )
                {
                    foreach( var item in result )
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
