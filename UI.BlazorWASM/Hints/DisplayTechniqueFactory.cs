using SmartSolver.SolvingTechniques;
using System;
using System.Linq;
using UI.BlazorWASM.Hints.SolvingTechniqueDisplayers;

namespace UI.BlazorWASM.Hints
{
    public static class DisplayTechniqueFactory
    {
        public static IDisplaySolvingTechnique GetDisplayer(ISolvingTechnique technique)
        {
            if( technique == null ) return new NotFoundDisplayer();

            var tuples = new[]{
                (typeof(NoSolution), typeof(NoSolutionDisplayer)),
                (typeof(InvalidValue), typeof(InvalidValuesDisplayer)),
                (typeof(CandidateMissing), typeof(CandidateMissingDisplayer)),
                (typeof(NakedSingle), typeof(NakedSingleDisplayer)),
                (typeof(FullHouse), typeof(FullHouseDisplayer)),
                (typeof(HiddenSingle), typeof(HiddenSingleDisplayer)),
                (typeof(NakedPair), typeof(NakedPairDisplayer)),
                (typeof(NakedSubset), typeof(NakedSubsetDisplayer)),
                (typeof(HiddenPair), typeof(HiddenPairDisplayer)),
                (typeof(LockedCandidatesPointing), typeof(LockedCandidatesPointingDisplayer)),
                (typeof(LockedCandidatesClaiming), typeof(LockedCandidatesClaimingDisplayer)),
                (typeof(Skyscrapper), typeof(SkyscrapperDisplayer)),
                (typeof(XWing), typeof(XWingDisplayer)),
                (typeof(XYWing), typeof(XYWingDisplayer)),
            };

            var tuple = tuples.FirstOrDefault(tuple => technique.GetType() == tuple.Item1);

            if (tuple == default) return new NotFoundDisplayer();

            return (IDisplaySolvingTechnique) Activator.CreateInstance(tuple.Item2, technique);
        }
    }
}
