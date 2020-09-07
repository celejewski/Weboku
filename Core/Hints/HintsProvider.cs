using Core.Data;
using Core.Hints.SolvingTechniques;
using Core.Hints.TechniqueFinders;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Core.Hints
{
    public class HintsProvider
    {
        public HintsProvider()
        {
            _finders = new ITechniqueFinder[]
            {
                new InvalidValueFinder(),
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

        public ISolvingTechnique GetNextHint(IGrid grid)
        {
            var stopwatch = Stopwatch.StartNew();

            var step = GetAllHints(grid)
#if DEBUG
                .ToList()
#endif
                .FirstOrDefault();

#if DEBUG
            System.Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms - total");
#endif

            return step;
        }

        public IEnumerable<ISolvingTechnique> GetAllHints(IGrid grid)
        {
            foreach( var finder in _finders )
            {
                var techniques = finder.FindAll(grid);
#if DEBUG
                var stopwatchFindAll = Stopwatch.StartNew();
                techniques = techniques.ToList();
                stopwatchFindAll.Stop();
                global::System.Console.WriteLine($"{stopwatchFindAll.ElapsedMilliseconds}ms - {finder.GetType()} - {techniques.Count()}");
                var stopwatchCanExecute = Stopwatch.StartNew();
#endif

                foreach( var technique in techniques )
                {
                    if( technique.CanExecute(grid) )
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
