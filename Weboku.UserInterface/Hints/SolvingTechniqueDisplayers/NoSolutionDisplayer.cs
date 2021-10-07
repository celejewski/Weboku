using Weboku.Application;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class NoSolutionDisplayer : BaseSolvingTechniqueDisplayer
    {
        public NoSolutionDisplayer(DomainFacade displayer, ISolvingTechnique noSolution)
            : base(displayer, noSolution, "no-solution")
        {
        }
    }
}