using System.Collections.Generic;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class CandidateMissingFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            var candidateMissing = new CandidateMissing();
            if (candidateMissing.CanExecute(grid))
            {
                yield return candidateMissing;
            }
        }
    }
}