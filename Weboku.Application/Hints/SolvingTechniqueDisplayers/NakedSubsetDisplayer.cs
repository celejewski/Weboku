﻿using System.Collections.Generic;
using System.Linq;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Hints;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Application.Hints.SolvingTechniqueDisplayers
{
    public class NakedSubsetDisplayer : BaseSolvingTechniqueDisplayer
    {
        private Position Pos => _positions.First();
        protected readonly IEnumerable<Position> _positions;
        protected readonly IEnumerable<Value> _values;

        public NakedSubsetDisplayer(DomainFacade displayer, NakedSubset nakedSubset)
            : base(displayer, nakedSubset, "naked-subset")
        {
            _positions = nakedSubset.Positions;
            _values = nakedSubset.Values;
        }

        public override void DisplaySolution()
        {
            base.DisplaySolution();
            foreach (var value in _values)
            {
                _displayer.MarkIfHasCandidate(Color.Illegal, GetPositionsToRemove(_informer), value);
                _displayer.MarkIfHasCandidate(Color.Legal, _positions, value);
            }

            foreach (var house in GetHouses())
            {
                _displayer.HighlightHouse(Pos, house);
            }

            _displayer.SetValueFilter(Value.None);
        }

        private IEnumerable<House> GetHouses()
        {
            return HintsHelper.GetHouses(_positions);
        }

        private IEnumerable<Position> GetPositionsToRemove(DomainFacade informer)
        {
            var positionsInHouses = GetHouses()
                .SelectMany(house => HintsHelper.GetPositionsInHouse(Pos, house));

            return positionsInHouses
                .Where(pos => _values.Any(value => informer.HasCandidate(pos, value)))
                .Except(_positions);
        }
    }
}