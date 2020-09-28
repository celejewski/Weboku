using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public abstract class NakedSubsetFinderBase : TechniqueFinderBase
    {
        protected IEnumerable<(IEnumerable<Position> positions, IEnumerable<Value> values)> NakedSubset(Grid grid, int depth)
        {
            foreach( var house in Position.Houses )
            {
                var positionsInHouse = house
                    .Where(pos => grid.GetCandidates(pos).Count() > 0
                        && grid.GetCandidates(pos).Count() <= depth)
                    .ToList();

                if( positionsInHouse.Count < depth ) continue;

                for( int i = 0; i <= positionsInHouse.Count - depth; i++ )
                {
                    foreach( var item in NakedSubsetStep(grid, new List<Position>(), Candidates.None, positionsInHouse, depth, i) )
                    {
                        yield return item;
                    }
                }
            }
        }

        private IEnumerable<(IEnumerable<Position> positions, IEnumerable<Value> values)> NakedSubsetStep(
            Grid grid,
            List<Position> positions,
            Candidates values,
            List<Position> house,
            int depth,
            int startingIndex)
        {
            if( positions.Count + house.Count - startingIndex < depth ) yield break;
            if( positions.Count == depth )
            {
                foreach( var pos in house.Except(positions) )
                {
                    var candidates = grid.GetCandidates(pos);
                    if( (candidates & values) != Candidates.None )
                    {
                        yield return (new List<Position>(positions), values.ToValues());
                        break;
                    }
                }
                yield break;
            }

            for( int index = startingIndex; index < house.Count; index++ )
            {
                var pos = house[index];
                var valuesNew = values | grid.GetCandidates(pos);
                if( valuesNew.Count() > depth ) continue;
                positions.Add(pos);
                foreach( var item in NakedSubsetStep(grid, positions, valuesNew, house, depth, index + 1) )
                {
                    yield return item;
                }
                positions.RemoveAt(positions.Count - 1);
            }
        }
    }
}
