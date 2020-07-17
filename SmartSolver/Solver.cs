using Core.Data;
using SmartSolver.SolvingTechniques;
using SmartSolver.TechniqueFinders;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace SmartSolver
{
    public class Solver
    {
        private readonly ISolvingTechniquesFactory _factory;

        public Solver(ISolvingTechniquesFactory factory)
        {
            _factory = factory;

            _finders = new ITechniqueFinder[]
            {
                new FullHouseFinder(_factory),
                new NakedSingleFinder(_factory),
                new HiddenSingleFinder(_factory),
                new LockedCandidatesPointingFinder(_factory),
                new LockedCandidatesClaimingFinder(_factory),
                new HiddenPairFinder(_factory),
            };
        }

        private readonly IEnumerable<ITechniqueFinder> _finders;

        public ISolvingTechnique NextStep(IGrid grid)
        {
            return AllSteps(grid).ToList().FirstOrDefault();
        }

        public IEnumerable<ISolvingTechnique> AllSteps(IGrid grid)
        {
            foreach( var finder in _finders )
            {
                foreach( var technique in finder.FindAll(grid) )
                {
                    if( technique.CanExecute(grid) )
                    {
#if DEBUG
                        System.Console.WriteLine(technique);
#endif
                        yield return technique;
                    }
                }
            }
        }
    }
}
