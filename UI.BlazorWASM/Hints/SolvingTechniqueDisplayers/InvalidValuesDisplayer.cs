using Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class InvalidValuesDisplayer : BaseSolvingTechniqueDisplayer
    {
        public InvalidValuesDisplayer(Informer informer, Displayer displayer, InvalidValue invalidValue)
            : base(informer, displayer, invalidValue, "invalid-solution")
        {
        }
    }
}