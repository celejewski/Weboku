using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class NakedSingleFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            for (int i = 0; i < Position.Positions.Count; i++)
            {
                var position = Position.Positions[i];
                if (grid.GetCandidates(position).Count() != 1) continue;

                var value = grid
                    .GetCandidates(position)
                    .ToValues()
                    .Single();

                yield return new NakedSingle(position, value);
            }
        }
    }
}