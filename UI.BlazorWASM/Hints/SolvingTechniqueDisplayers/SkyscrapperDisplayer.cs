using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class SkyscrapperDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Position _base1;
        private readonly Position _base2;
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value;

        public SkyscrapperDisplayer(Informer informer, Displayer displayer, Skyscrapper skyscrapper) 
            : base(informer, displayer, skyscrapper, "skyscrapper")
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

            _displayer.Mark(Enums.Color.Legal, _pos1, _value);
            _displayer.Mark(Enums.Color.Legal, _pos2, _value);
            _displayer.Mark(Enums.Color.Legal, _base1, _value);
            _displayer.Mark(Enums.Color.Legal, _base2, _value);
            _displayer.MarkIfHasCandidate(Enums.Color.Illegal, Position.GetOtherPositionsSeenBy(_pos1, _pos2), _value);

            _displayer.HighlightHouse(_base1, Position.GetHouseOf(_base1, _base2));
            _displayer.HighlightHouse(_pos1, Position.GetHouseOf(_pos1, _base1));
            _displayer.HighlightHouse(_pos2, Position.GetHouseOf(_pos2, _base2));

            _displayer.SetValueFilter(_value);
        }

    }
}
