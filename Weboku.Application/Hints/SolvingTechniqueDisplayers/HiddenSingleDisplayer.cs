﻿using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Hints;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Application.Hints.SolvingTechniqueDisplayers
{
    public class HiddenSingleDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Position _position;
        private readonly Value _value;

        private readonly House _house;
        private string _houseFormated;

        public HiddenSingleDisplayer(DomainFacade displayer, HiddenSingle hiddenSingle)
            : base(displayer, hiddenSingle, "hidden-single")
        {
            _position = hiddenSingle.Position;
            _value = hiddenSingle.Value;
            _house = hiddenSingle.House;

            _explanationSteps.AddRange(new Action[] {Explain1, Explain2, Explain3});
        }

        private void SetupDisplayer()
        {
            _displayer.SetTitle(TitleKey);
            _displayer.HighlightHouse(_position, _house);

            _houseFormated = _displayer.Format(_house, _position);
        }

        public override void DisplaySolution()
        {
            SetupDisplayer();

            _displayer.SetDescription(DescriptionKey, _displayer.Format(_house, _position), _value, _position);
            _displayer.SetValueFilter(_value);

            _displayer.MarkCells(Color.Illegal, HintsHelper.GetPositionsInHouse(_position, _house));
            _displayer.MarkCell(Color.Legal, _position);
        }

        private void Explain1()
        {
            SetupDisplayer();
            _displayer.SetValueFilter(Value.None);
            _displayer.SetDescription(ExplanationKey(1), _houseFormated);
        }

        private void Explain2()
        {
            SetupDisplayer();
            _displayer.SetValueFilter(_value);
            _displayer.SetDescription(ExplanationKey(2), _value, _houseFormated, _position);
        }

        private void Explain3()
        {
            SetupDisplayer();
            var posInHouse = HintsHelper.GetPositionsInHouse(_position, _house);
            _displayer.MarkInputOrCandidate(Color.Illegal, posInHouse, _value);
            _displayer.MarkCells(Color.Illegal, posInHouse);
            _displayer.Mark(Color.Legal, _position, _value);
            _displayer.SetDescription(ExplanationKey(3), _value, _value, _houseFormated, _position);
        }
    }
}