using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace Core.Hints.TechniqueFinders
{
    public class NakedTripleFinder : NakedSubsetFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach( var (positions, values) in NakedSubset(grid, 3) )
            {
                yield return new NakedSubset(positions, values);
            }
        }
    }
}
