using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;

namespace SmartSolver.TechniqueFinders
{
    public class HiddenPairFinder : BaseHiddenSubsetFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public HiddenPairFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }
        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach (var result in HiddenSubset(grid, 2))
            {
                yield return _factory.HiddenPair(result.positions, result.values);
            }
        }
    }
}
