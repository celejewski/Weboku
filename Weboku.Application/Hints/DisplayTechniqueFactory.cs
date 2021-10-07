using System;
using System.Linq;
using Weboku.Application.Hints.SolvingTechniqueDisplayers;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Application.Hints
{
    public static class DisplayTechniqueFactory
    {
        public static ISolvingTechniqueDisplayer MakeDisplayer(DomainFacade domainFacade, ISolvingTechnique technique)
        {
            if (technique == null) return new NotFoundDisplayer(domainFacade);

            var tuples = new[]
            {
                (typeof(NoSolution), typeof(NoSolutionDisplayer)),
                (typeof(InvalidValue), typeof(InvalidValuesDisplayer)),
                (typeof(CandidateMissing), typeof(CandidateMissingDisplayer)),
                (typeof(NakedSingle), typeof(NakedSingleDisplayer)),
                (typeof(FullHouse), typeof(FullHouseDisplayer)),
                (typeof(HiddenSingle), typeof(HiddenSingleDisplayer)),
                (typeof(NakedPair), typeof(NakedPairDisplayer)),
                (typeof(NakedSubset), typeof(NakedSubsetDisplayer)),
                (typeof(HiddenPair), typeof(HiddenPairDisplayer)),
                (typeof(HiddenSubset), typeof(HiddenSubsetDisplayer)),
                (typeof(LockedCandidatesPointing), typeof(LockedCandidatesPointingDisplayer)),
                (typeof(LockedCandidatesClaiming), typeof(LockedCandidatesClaimingDisplayer)),
                (typeof(Skyscrapper), typeof(SkyscrapperDisplayer)),
                (typeof(XWing), typeof(XWingDisplayer)),
                (typeof(XYWing), typeof(XYWingDisplayer)),
                (typeof(TwoStringKite), typeof(TwoStringKiteDisplayer)),
            };

            var tuple = tuples.FirstOrDefault(tuple => technique.GetType() == tuple.Item1);

            if (tuple == default) return new NotFoundDisplayer(domainFacade);

            return (ISolvingTechniqueDisplayer) Activator.CreateInstance(tuple.Item2, domainFacade, technique);
        }
    }
}