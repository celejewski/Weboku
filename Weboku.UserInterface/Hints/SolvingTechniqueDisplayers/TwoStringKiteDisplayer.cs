using Weboku.Application;
using Weboku.Application.Enums;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class TwoStringKiteDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly DomainFacade _displayer;
        private readonly TwoStringKite _twoStringKite;

        public TwoStringKiteDisplayer(DomainFacade displayer, TwoStringKite twoStringKite)
            : base(displayer, twoStringKite, "two-string-kite")
        {
            _displayer = displayer;
            _twoStringKite = twoStringKite;
        }

        public override void DisplaySolution()
        {
            base.DisplaySolution();
            _displayer.Mark(Color.Legal, _twoStringKite.LegalPositions, _twoStringKite.Value);
            _displayer.Mark(Color.Info, _twoStringKite.InfoPositions, _twoStringKite.Value);
            _displayer.Mark(Color.Illegal, _twoStringKite.PositionToRemove, _twoStringKite.Value);
            _displayer.SetValueFilter(_twoStringKite.Value);
        }
    }
}