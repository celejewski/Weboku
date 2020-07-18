﻿using Core.Data;
using SmartSolver.SolvingTechniques;
using SmartSolver.TechniqueFinders;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
                new InvalidValueFinder(_factory),
                new CandidateMissingFinder(_factory),
                new FullHouseFinder(_factory),
                new HiddenSingleFinder(_factory),
                new NakedSingleFinder(_factory),
                new LockedCandidatesPointingFinder(_factory),
                new LockedCandidatesClaimingFinder(_factory),
                new NakedPairFinder(_factory),
                new NakedTripleFinder(_factory),
                new HiddenPairFinder(_factory),
                new XWingFinder(_factory),
                new SkyscrapperFinder(_factory),
                new XYWingFinder(_factory),
                new NakedQuadrupleFinder(_factory),
                new HiddenTripleFinder(_factory),
            };
        }

        private readonly IEnumerable<ITechniqueFinder> _finders;

        public ISolvingTechnique NextStep(IGrid grid)
        {
            var stopwatch = Stopwatch.StartNew();

            var step = AllSteps(grid)
#if DEBUG
                .ToList()
#endif
                .FirstOrDefault();

            System.Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms elapsed");


            return step;
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
