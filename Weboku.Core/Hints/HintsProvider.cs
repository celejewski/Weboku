﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;
using Weboku.Core.Hints.TechniqueFinders;
using Weboku.Core.Validators;

namespace Weboku.Core.Hints
{
    public class HintsProvider
    {
        public HintsProvider()
        {
            _finders = new ITechniqueFinder[]
            {
                new FullHouseFinder(),
                new HiddenSingleWithoutCandidatesFinder(),
                new CandidateMissingFinder(),
                new HiddenSingleFinder(),
                new NakedSingleFinder(),
                new LockedCandidatesPointingFinder(),
                new LockedCandidatesClaimingFinder(),
                new NakedPairFinder(),
                new NakedTripleFinder(),
                new HiddenPairFinder(),
                new XWingFinder(),
                new SkyscrapperFinder(),
                new TwoStringKiteFinder(),
                new NakedQuadrupleFinder(),
                new XYWingFinder(),
                new HiddenTripleFinder(),
            };
        }

        private readonly IEnumerable<ITechniqueFinder> _finders;

        public ISolvingTechnique GetNextHint(Grid grid)
        {
            var stopwatch = Stopwatch.StartNew();

            if (!ValidatorGrid.AreAllGivensLegal(grid))
            {
                var positionsWithInvalidValues = Position.Positions
                    .Where(position => !grid.GetIsGiven(position)
                                       && !grid.IsCandidateLegal(position, grid.GetValue(position)));
                return new InvalidValue(positionsWithInvalidValues.ToList());
            }

            var step = GetAllHints(grid)
#if DEBUG
                .ToList()
#endif
                .FirstOrDefault();

#if DEBUG
            System.Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms - total");
#endif

            return step ?? new HintNotFound();
        }

        private IEnumerable<ISolvingTechnique> GetAllHints(Grid grid)
        {
            foreach (var finder in _finders)
            {
                var techniques = finder.FindAll(grid);
#if DEBUG
                var stopwatchFindAll = Stopwatch.StartNew();
                techniques = techniques.ToList();
                stopwatchFindAll.Stop();
                global::System.Console.WriteLine($"{stopwatchFindAll.ElapsedMilliseconds}ms - {finder.GetType()} - {techniques.Count()} - ticks: {stopwatchFindAll.ElapsedTicks}ticks");
                var stopwatchCanExecute = Stopwatch.StartNew();
#endif

                foreach (var technique in techniques)
                {
                    if (technique.CanExecute(grid))
                    {
#if DEBUG
                        System.Console.WriteLine(technique);
#endif
                        yield return technique;
                    }
                }

#if DEBUG
                stopwatchCanExecute.Stop();
                if (techniques.Any()) System.Console.WriteLine($"{stopwatchCanExecute.ElapsedMilliseconds}ms - CanExecute");
#endif
            }
        }
    }
}