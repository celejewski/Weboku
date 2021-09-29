using System.Collections.Generic;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class HiddenTripleFinder : HiddenSubsetFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach (var (positions, values) in HiddenSubset(grid, 3))
            {
                yield return new HiddenSubset(positions, values);
            }
        }
    }
}