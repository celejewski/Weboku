using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace Core.Hints.TechniqueFinders
{
    public class HiddenPairFinder : HiddenSubsetFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach( var (positions, values) in HiddenSubset(grid, 2) )
            {
                yield return new HiddenPair(positions, values);
            }
        }
    }
}
