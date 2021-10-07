using Weboku.Application;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class NotFoundDisplayer : BaseSolvingTechniqueDisplayer
    {
        public NotFoundDisplayer(DomainFacade displayer) : base(displayer, "no-hint")
        {
        }
    }
}