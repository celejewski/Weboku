using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public class NakedSingleFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            for( int i = 0; i < Position.Positions.Count; i++ )
            {
                var position = Position.Positions[i];
                if( grid.GetCandidates(position).Count() != 1 ) continue;

                var value = grid
                    .GetCandidates(position)
                    .ToValues()
                    .Single();

                yield return new NakedSingle(position, value);
            }
        }
    }
}
