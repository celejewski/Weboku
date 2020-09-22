using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public abstract class HiddenSubsetFinderBase : TechniqueFinderBase
    {
        protected IEnumerable<(IEnumerable<Position> positions, IEnumerable<Value> values)> HiddenSubset(IGrid input, int depth)
        {
            foreach( var house in Position.Houses )
            {
                var candidatePositions = new Dictionary<Value, IEnumerable<Position>>();
                foreach( var value in Value.NonEmpty )
                {
                    var positionsWithCandidate = house.Where(pos => input.HasCandidate(pos, value)).ToList();
                    if( positionsWithCandidate.Count > 0
                        && depth >= positionsWithCandidate.Count )
                    {
                        candidatePositions[value] = positionsWithCandidate;
                    }
                }

                var result = HiddenSubsetStep(input, candidatePositions, new Dictionary<Position, int>(), Candidates.None, 0, depth);
                foreach( var item in result )
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<(IEnumerable<Position> positions, IEnumerable<Value> values)> HiddenSubsetStep(
            IGrid input,
            Dictionary<Value, IEnumerable<Position>> house,
            Dictionary<Position, int> positions,
            Candidates values,
            int startingIndex,
            int depth)
        {
            if( positions.Count > depth
                || values.Count() + house.Count - startingIndex <= depth
                || positions.Keys.Any(pos => (input.GetCandidates(pos) & values) == 0) )
            {
                yield break;
            }
            if( values.Count() == depth )
            {
                if( positions.Keys.Any(pos => (input.GetCandidates(pos) & ~values) != 0) )
                {
                    yield return (positions.Keys.ToList(), values.ToValues());
                }
                yield break;
            }

            for( int i = startingIndex; i < house.Keys.Count; i++ )
            {
                var value = house.Keys.ElementAt(i);

                foreach( var pos in house[value] )
                {
                    if( !positions.ContainsKey(pos) )
                    {
                        positions[pos] = i;
                    }
                }
                var valuesNew = values | value.AsCandidates();
                foreach( var item in HiddenSubsetStep(input, house, positions, valuesNew, i + 1, depth) )
                {
                    yield return item;
                }

                foreach( var pos in house[value] )
                {
                    if( positions[pos] == i )
                    {
                        positions.Remove(pos);
                    }
                }
            }
        }
    }
}
