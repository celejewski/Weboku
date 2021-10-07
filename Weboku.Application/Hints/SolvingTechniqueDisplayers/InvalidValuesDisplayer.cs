using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Application.Hints.SolvingTechniqueDisplayers
{
    public class InvalidValuesDisplayer : BaseSolvingTechniqueDisplayer
    {
        public InvalidValuesDisplayer(DomainFacade displayer, InvalidValue invalidValue)
            : base(displayer, invalidValue, "invalid-solution")
        {
        }
    }
}