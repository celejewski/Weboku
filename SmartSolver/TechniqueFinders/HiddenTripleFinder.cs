using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;

namespace SmartSolver.TechniqueFinders
{
    public class HiddenTripleFinder : BaseHiddenSubsetFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public HiddenTripleFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var result in HiddenSubset(grid, 3) )
            {
                yield return _factory.HiddenSubset(result.positions, result.values);
            }
        }
    }
}
