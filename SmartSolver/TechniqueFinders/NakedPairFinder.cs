using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;

namespace SmartSolver.TechniqueFinders
{
    public class NakedPairFinder : BaseNakedSubsetFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public NakedPairFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach (var item in NakedSubset(grid, 2))
            {
                yield return _factory.NakedPair(item.positions, item.values);
            }
        }
    }
}
