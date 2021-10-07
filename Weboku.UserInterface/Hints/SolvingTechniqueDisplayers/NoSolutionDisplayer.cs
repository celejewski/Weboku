using Weboku.Application;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class NoSolutionDisplayer : BaseSolvingTechniqueDisplayer
    {
        public NoSolutionDisplayer(Informer informer, DomainFacade displayer, ISolvingTechnique noSolution)
            : base(informer, displayer, noSolution, "no-solution")
        {
        }
    }
}