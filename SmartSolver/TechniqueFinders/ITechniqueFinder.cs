using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;

namespace SmartSolver.TechniqueFinders
{
    public interface ITechniqueFinder
    {
        public ISolvingTechnique Find(IGrid grid);
        public IEnumerable<ISolvingTechnique> FindAll(IGrid grid);
    }
}
