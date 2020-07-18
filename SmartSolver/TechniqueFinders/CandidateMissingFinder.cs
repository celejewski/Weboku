using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSolver.TechniqueFinders
{
    public class CandidateMissingFinder : BaseTechniqueFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public CandidateMissingFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            var candidateMissing = _factory.CandidateMissing();
            if (candidateMissing.CanExecute(grid))
            {
                yield return candidateMissing;
            }
        }
    }
}
