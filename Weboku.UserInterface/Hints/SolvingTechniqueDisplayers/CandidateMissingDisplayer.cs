using Weboku.Application;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class CandidateMissingDisplayer : BaseSolvingTechniqueDisplayer
    {
        public CandidateMissingDisplayer(DomainFacade displayer, CandidateMissing candidateMissing)
            : base(displayer, candidateMissing, "candidates-missing")
        {
        }
    }
}