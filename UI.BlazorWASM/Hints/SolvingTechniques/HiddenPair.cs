﻿using Core.Data;
using System.Collections.Generic;
using UI.BlazorWASM.Helpers;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class HiddenPair : HiddenSubset
    {
        private readonly string _houseFormatted;
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value1;
        private readonly InputValue _value2;

        public HiddenPair(IEnumerable<Position> positions, IEnumerable<InputValue> values) 
            :base(positions, values)
        {
            _locKey = "hidden-pair";
            _houseFormatted = Displayer.Format(_house, Position);

            _pos1 = positions.ElementAt(0);
            _pos2 = positions.ElementAt(1);
            _value1 = values.ElementAt(0);
            _value2 = values.ElementAt(1);
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);

            displayer.SetDescription("hidden-pair__description", 
                _houseFormatted, _value1, _value2, _pos1, _pos2);
            
        }
    }
}
