using Weboku.Application;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class CandidateMissingDisplayer : BaseSolvingTechniqueDisplayer
    {
        public CandidateMissingDisplayer(Informer informer, DomainFacade displayer, CandidateMissing candidateMissing)
            : base(informer, displayer, candidateMissing, "candidates-missing")
        {
        }
    }
}