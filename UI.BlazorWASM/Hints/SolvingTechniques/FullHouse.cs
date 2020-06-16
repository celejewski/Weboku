﻿using Core.Data;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FullHouse : BaseSolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _value;

        public FullHouse(Position position, InputValue value) 
            : base("Full House")
        {
            _position = position;
            _value = value;
        }

        public override bool CanExecute(Informer informer)
        {
            return informer.HasCandidate(_position, _value);
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {

            var house = HintsHelper.FindHouse(_position,
                positions => positions.Where(pos => !informer.HasValue(pos))
                                      .Count() == 1
                );

            displayer.SetTitle(_title);
            displayer.SetDescription($"In {HintsHelper.Format(house, _position)} cell {_position} is the last one without value.");
            displayer.MarkCell(Enums.Color.Legal, _position);
            displayer.MarkCandidate(Enums.Color.Legal, _position, _value);
            displayer.HighlightHouse(_position, house);
            displayer.SetValueFilter(_value);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.SetInput(_value, _position);
        }
    }
}
