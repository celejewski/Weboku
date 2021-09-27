using Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class NoSolutionDisplayer : BaseSolvingTechniqueDisplayer
    {
        public NoSolutionDisplayer(Informer informer, Displayer displayer, ISolvingTechnique noSolution)
            : base(informer, displayer, noSolution, "no-solution")
        {
        }
    }
}