using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public class NakedSingleFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            for( int i = 0; i < Position.All.Count; i++ )
            {
                var position = Position.All[i];
                if( grid.GetCandidates(position).Count() != 1 ) continue;

                var value = grid
                    .GetCandidates(position)
                    .ToInputValues()
                    .Single();

                yield return new NakedSingle(position, value);
            }
        }
    }
}
