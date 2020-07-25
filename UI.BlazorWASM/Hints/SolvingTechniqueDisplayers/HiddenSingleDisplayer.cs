using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class HiddenSingleDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Position _position;
        private readonly InputValue _inputValue;

        private readonly House _house;
        private string _houseFormated;
        public HiddenSingleDisplayer(Informer informer, Displayer displayer, HiddenSingle hiddenSingle)
            : base(informer, displayer, hiddenSingle, "hidden-single")
        {
            _position = hiddenSingle.Position;
            _inputValue = hiddenSingle.Value;
            _house = hiddenSingle.House;

            _explanationSteps.AddRange(new Action[] { Explain1, Explain2, Explain3 });
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

            _displayer.SetDescription(DescriptionKey, _displayer.Format(_house, _position), _inputValue, _position);
            _displayer.SetValueFilter(_inputValue);

            _displayer.MarkCells(Enums.Color.Illegal, HintsHelper.GetPositionsInHouse(_position, _house));
            _displayer.MarkCell(Enums.Color.Legal, _position);
        }

        private void Explain1()
        {
            SetupDisplayer();
            _displayer.SetValueFilter(InputValue.Empty);
            _displayer.SetDescription(ExplanationKey(1), _houseFormated);
        }

        private void Explain2()
        {
            SetupDisplayer();
            _displayer.SetValueFilter(_inputValue);
            _displayer.SetDescription(ExplanationKey(2), _inputValue, _houseFormated, _position);
        }
        private void Explain3()
        {
            SetupDisplayer();
            var posInHouse = HintsHelper.GetPositionsInHouse(_position, _house);
            _displayer.MarkInputOrCandidate(Enums.Color.Illegal, posInHouse, _inputValue);
            _displayer.MarkCells(Enums.Color.Illegal, posInHouse);
            _displayer.Mark(Enums.Color.Legal, _position, _inputValue);
            _displayer.SetDescription(ExplanationKey(3), _inputValue, _inputValue, _houseFormated, _position);
        }
    }
}
