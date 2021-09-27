using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public abstract class TechniqueFinderBase : ITechniqueFinder
    {
        public ISolvingTechnique Find(Grid grid)
        {
            return FindAll(grid).First();
        }

        public abstract IEnumerable<ISolvingTechnique> FindAll(Grid grid);
    }
}