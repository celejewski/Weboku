﻿using System.Collections.Generic;
using System.Linq;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Application.Hints.SolvingTechniqueDisplayers
{
    public class XWingDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Value _value;
        private readonly IEnumerable<Position> _positions;
        private readonly IEnumerable<Position> _positionsToRemove;
        private readonly House _house;

        public XWingDisplayer(DomainFacade displayer, XWing xWing)
            : base(displayer, xWing, "xwing")
        {
            _value = xWing.Value;
            _positions = xWing.Positions;
            _positionsToRemove = xWing.PositionsToRemove;
            _house = xWing.House;
        }

        public override void DisplaySolution()
        {
            base.DisplaySolution();

            var opositeHouse = _house == House.Col ? House.Row : House.Col;
            _displayer.SetDescription(
                DescriptionKey,
                _displayer.Format(_house, _positions.First()),
                _displayer.Format(_house, _positions.Last()),
                _value,
                _displayer.Format(opositeHouse, _positions.First()),
                _displayer.Format(opositeHouse, _positions.Last())
            );
            _displayer.SetValueFilter(_value);

            foreach (var pos in _positions)
            {
                _displayer.HighlightCol(pos);
                _displayer.HighlightRow(pos);
            }

            _displayer.MarkCandidates(Color.Legal, _positions, _value);
            _displayer.MarkCandidates(Color.Illegal, _positionsToRemove, _value);
        }
    }
}