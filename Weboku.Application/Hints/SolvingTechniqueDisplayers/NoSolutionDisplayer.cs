using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Application.Hints.SolvingTechniqueDisplayers
{
    public class NoSolutionDisplayer : BaseSolvingTechniqueDisplayer
    {
        public NoSolutionDisplayer(DomainFacade displayer, ISolvingTechnique noSolution)
            : base(displayer, noSolution, "no-solution")
        {
        }
    }
}