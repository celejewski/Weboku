using Weboku.Application;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class InvalidValuesDisplayer : BaseSolvingTechniqueDisplayer
    {
        public InvalidValuesDisplayer(Informer informer, DomainFacade displayer, InvalidValue invalidValue)
            : base(informer, displayer, invalidValue, "invalid-solution")
        {
        }
    }
}