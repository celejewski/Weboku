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
        public HiddenSingleDisplayer(HiddenSingle hiddenSingle)
            : base(hiddenSingle, "hidden-single")
        {
            _position = hiddenSingle.Position;
            _inputValue = hiddenSingle.Value;
            _house = hiddenSingle.House;

            _explanationSteps.AddRange(new Action<Displayer, Informer>[] { Explain1, Explain2, Explain3 });
        }

        private void SetupDisplayer(Displayer displayer, Informer informer)
        {
            displayer.SetTitle(TitleKey);
            displayer.HighlightHouse(_position, _house);

            _houseFormated = displayer.Format(_house, _position);
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);

            displayer.SetDescription(DescriptionKey, displayer.Format(_house, _position), _inputValue, _position);
            displayer.SetValueFilter(_inputValue);

            displayer.MarkCells(Enums.Color.Illegal, HintsHelper.GetPositionsInHouse(_position, _house));
            displayer.MarkCell(Enums.Color.Legal, _position);
        }

        private void Explain1(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            displayer.SetValueFilter(InputValue.Empty);
            displayer.SetDescription(ExplanationKey(1), _houseFormated);
        }

        private void Explain2(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            displayer.SetValueFilter(_inputValue);
            displayer.SetDescription(ExplanationKey(2), _inputValue, _houseFormated, _position);
        }
        private void Explain3(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            var posInHouse = HintsHelper.GetPositionsInHouse(_position, _house);
            displayer.MarkInputOrCandidate(Enums.Color.Illegal, posInHouse, _inputValue);
            displayer.MarkCells(Enums.Color.Illegal, posInHouse);
            displayer.Mark(Enums.Color.Legal, _position, _inputValue);
            displayer.SetDescription(ExplanationKey(3), _inputValue, _inputValue, _houseFormated, _position);
        }
    }
}
