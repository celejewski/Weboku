using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace Core.Hints.TechniqueFinders
{
    public class HiddenTripleFinder : HiddenSubsetFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach( var (positions, values) in HiddenSubset(grid, 3) )
            {
                yield return new HiddenSubset(positions, values);
            }
        }
    }
}
