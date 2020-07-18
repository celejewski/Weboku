using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;

namespace SmartSolver.TechniqueFinders
{
    public class NakedQuadrupleFinder : BaseNakedSubsetFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public NakedQuadrupleFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var item in NakedSubset(grid, 4) )
            {
                yield return _factory.NakedSubset(item.positions, item.values);
            }
        }
    }
}
