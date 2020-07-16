using Core.Data;
using Core.Helpers;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public class HiddenSingleFinder : BaseTechniqueFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public HiddenSingleFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var positions in Position.Houses )
            {
                foreach( var value in InputValue.NonEmpty )
                {
                    var positionsWithCandidate = positions.WithCandidate(grid, value);
                    if( positionsWithCandidate.Count() == 1 )
                    {
                        var pos = positionsWithCandidate.First();
                        yield return _factory.HiddenSingle(pos, value);
                    }
                }
            }
        }
    }
}
