using SmartSolver.SolvingTechniques;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class CandidateMissingDisplayer : BaseSolvingTechniqueDisplayer
    {
        public CandidateMissingDisplayer(CandidateMissing candidateMissing) 
            : base(candidateMissing, "candidates-missing")
        {
        }
    }
}
