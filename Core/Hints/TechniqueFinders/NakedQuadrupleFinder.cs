using System.Collections.Generic;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class NakedQuadrupleFinder : NakedSubsetFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach (var (positions, values) in NakedSubset(grid, 4))
            {
                yield return new NakedSubset(positions, values);
            }
        }
    }
}