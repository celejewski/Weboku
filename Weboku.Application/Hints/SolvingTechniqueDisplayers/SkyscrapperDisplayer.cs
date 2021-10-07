using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Application.Hints.SolvingTechniqueDisplayers
{
    public class SkyscrapperDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Position _base1;
        private readonly Position _base2;
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly Value _value;

        public SkyscrapperDisplayer(DomainFacade displayer, Skyscrapper skyscrapper)
            : base(displayer, skyscrapper, "skyscrapper")
        {
            _base1 = skyscrapper.Base1;
            _base2 = skyscrapper.Base2;
            _pos1 = skyscrapper.Pos1;
            _pos2 = skyscrapper.Pos2;
            _value = skyscrapper.Value;
        }

        public override void DisplaySolution()
        {
            base.DisplaySolution();

            _displayer.Mark(Color.Legal, _pos1, _value);
            _displayer.Mark(Color.Legal, _pos2, _value);
            _displayer.Mark(Color.Legal, _base1, _value);
            _displayer.Mark(Color.Legal, _base2, _value);
            _displayer.MarkIfHasCandidate(Color.Illegal, Position.GetOtherPositionsSeenBy(_pos1, _pos2), _value);

            _displayer.HighlightHouse(_base1, Position.GetHouseOf(_base1, _base2));
            _displayer.HighlightHouse(_pos1, Position.GetHouseOf(_pos1, _base1));
            _displayer.HighlightHouse(_pos2, Position.GetHouseOf(_pos2, _base2));

            _displayer.SetValueFilter(_value);
        }
    }
}