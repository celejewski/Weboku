using Weboku.Application;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class NotFoundDisplayer : BaseSolvingTechniqueDisplayer
    {
        public NotFoundDisplayer(Informer informer, DomainFacade displayer) : base(informer, displayer, "no-hint")
        {
        }
    }
}