using System.Collections.Generic;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public interface ITechniqueFinder
    {
        public ISolvingTechnique Find(Grid grid);
        public IEnumerable<ISolvingTechnique> FindAll(Grid grid);
    }
}