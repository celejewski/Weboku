﻿using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Application.Hints.SolvingTechniqueDisplayers
{
    public class NakedSingleDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Position _position;
        private readonly Value _value;

        public NakedSingleDisplayer(DomainFacade displayer, NakedSingle nakedSingle)
            : base(displayer, nakedSingle, "naked-single")
        {
            _position = nakedSingle.Position;
            _value = nakedSingle.Value;
        }

        public override void DisplaySolution()
        {
            _displayer.SetTitle(TitleKey);
            _displayer.SetDescription(DescriptionKey, _position, _value, _position);
            _displayer.Mark(Color.Legal, _position, _value);
            _displayer.HighlightBlock(_position);
            _displayer.HighlightCol(_position);
            _displayer.HighlightRow(_position);
            _displayer.SetValueFilter(_value);
        }
    }
}