using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public abstract class BaseTechniqueFinder : ITechniqueFinder
    {
        public ISolvingTechnique Find(IGrid grid)
        {
            return FindAll(grid).First();
        }

        public abstract IEnumerable<ISolvingTechnique> FindAll(IGrid grid);
    }
}
