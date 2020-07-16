using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public class NakedSingleFinder : BaseTechniqueFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public NakedSingleFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var pos in Position.All
                .Where(pos => grid.CandidatesCount(pos) == 1))
            {
                var value = InputValue.NonEmpty.First(value => grid.HasCandidate(pos, value));
                yield return _factory.NakedSingle(pos, value);   
            }
        }
    }
}
