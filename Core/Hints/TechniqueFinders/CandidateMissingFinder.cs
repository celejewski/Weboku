using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace Core.Hints.TechniqueFinders
{
    public class CandidateMissingFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            var candidateMissing = new CandidateMissing();
            if( candidateMissing.CanExecute(grid) )
            {
                yield return candidateMissing;
            }
        }
    }
}
