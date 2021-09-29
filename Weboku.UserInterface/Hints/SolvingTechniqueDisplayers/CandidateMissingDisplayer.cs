using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class CandidateMissingDisplayer : BaseSolvingTechniqueDisplayer
    {
        public CandidateMissingDisplayer(Informer informer, Displayer displayer, CandidateMissing candidateMissing)
            : base(informer, displayer, candidateMissing, "candidates-missing")
        {
        }
    }
}