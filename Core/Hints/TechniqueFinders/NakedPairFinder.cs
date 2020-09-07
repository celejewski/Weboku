using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace Core.Hints.TechniqueFinders
{
    public class NakedPairFinder : NakedSubsetFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var (positions, values) in NakedSubset(grid, 2) )
            {
                yield return new NakedPair(positions, values);
            }
        }
    }
}
