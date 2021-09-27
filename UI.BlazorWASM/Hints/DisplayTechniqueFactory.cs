using System;
using System.Linq;
using Core.Hints.SolvingTechniques;
using Weboku.UserInterface.Hints.SolvingTechniqueDisplayers;

namespace Weboku.UserInterface.Hints
{
    public static class DisplayTechniqueFactory
    {
        public static ISolvingTechniqueDisplayer MakeDisplayer(Informer informer, Displayer displayer, ISolvingTechnique technique)
        {
            if (technique == null) return new NotFoundDisplayer(informer, displayer);

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

            if (tuple == default) return new NotFoundDisplayer(informer, displayer);

            return (ISolvingTechniqueDisplayer) Activator.CreateInstance(tuple.Item2, informer, displayer, technique);
        }
    }
}