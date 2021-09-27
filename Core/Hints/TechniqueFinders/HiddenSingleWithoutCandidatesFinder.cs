using System.Collections.Generic;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class HiddenSingleWithoutCandidatesFinder : HiddenSingleFinder
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid input)
        {
            var grid = input.Clone();
            grid.FillAllLegalCandidates();
            return base.FindAll(grid);
        }
    }
}