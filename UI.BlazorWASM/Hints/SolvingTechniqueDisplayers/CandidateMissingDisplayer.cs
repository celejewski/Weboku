using SmartSolver.SolvingTechniques;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class CandidateMissingDisplayer : BaseSolvingTechniqueDisplayer
    {
        public CandidateMissingDisplayer(Informer informer, Displayer displayer, CandidateMissing candidateMissing) 
            : base(informer, displayer, candidateMissing, "candidates-missing")
        {
        }
    }
}
