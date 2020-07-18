using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class SkyscrapperDisplayer : BaseDisplaySolvingTechnique
    {
        private readonly Position _base1;
        private readonly Position _base2;
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value;

        public SkyscrapperDisplayer(Skyscrapper skyscrapper) 
            : base(skyscrapper, "skyscrapper")
        {
            _base1 = skyscrapper.Base1;
            _base2 = skyscrapper.Base2;
            _pos1 = skyscrapper.Pos1;
            _pos2 = skyscrapper.Pos2;
            _value = skyscrapper.Value;
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);

            displayer.Mark(Enums.Color.Legal, _pos1, _value);
            displayer.Mark(Enums.Color.Legal, _pos2, _value);
            displayer.Mark(Enums.Color.Legal, _base1, _value);
            displayer.Mark(Enums.Color.Legal, _base2, _value);
            displayer.MarkIfHasCandidate(Enums.Color.Illegal, Position.GetOtherPositionsSeenBy(_pos1, _pos2), _value);

            displayer.HighlightHouse(_base1, Position.GetHouse(_base1, _base2));
            displayer.HighlightHouse(_pos1, Position.GetHouse(_pos1, _base1));
            displayer.HighlightHouse(_pos2, Position.GetHouse(_pos2, _base2));

            displayer.SetValueFilter(_value);
        }

    }
}
