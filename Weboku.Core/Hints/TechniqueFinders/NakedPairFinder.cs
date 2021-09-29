using System.Collections.Generic;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class NakedPairFinder : NakedSubsetFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach (var (positions, values) in NakedSubset(grid, 2))
            {
                yield return new NakedPair(positions, values);
            }
        }
    }
}