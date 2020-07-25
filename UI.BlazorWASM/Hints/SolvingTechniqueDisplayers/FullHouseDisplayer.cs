using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class FullHouseDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Position _position;
        private readonly InputValue _value;

        private House _house;
        public FullHouseDisplayer(Informer informer, Displayer displayer, FullHouse fullHouse)
            : base(informer, displayer, fullHouse, "full-house")
        {
            _position = fullHouse.Position;
            _value = fullHouse.Value;

            _explanationSteps.Add(Explain1);

            for( int i = 0; i < _value - 1; i++ )
            {
                var explain = ExplainN(i);
                _explanationSteps.Add(explain);
            }
            _explanationSteps.Add(ExplainLast);
        }

        private void SetupDisplay()
        {
            _house = HintsHelper.HouseFirstOrDefault(_position,
                positions => positions.Where(pos => !_informer.HasValue(pos))
                                      .Count() == 1
                );

            _displayer.SetTitle(TitleKey);
            _displayer.HighlightHouse(_position, _house);
        }
        public override void DisplaySolution()
        {
            SetupDisplay();

            _displayer.SetDescription(DescriptionKey, _displayer.Format(_house, _position), _position);
            _displayer.Mark(Enums.Color.Legal, _position, _value);
            _displayer.SetValueFilter(_value);
        }

        public void Explain1()
        {
            SetupDisplay();

            _displayer.SetDescription(ExplanationKey("first"), _displayer.Format(_house, _position));
        }

        public Action ExplainN(InputValue n)
        {
            return () =>
            {
                SetupDisplay();
                var positions = HintsHelper.GetPositionsInHouse(_position, _house);
                var limit = n + 1;
                for( int i = 0; i < limit; i++ )
                {
                    _displayer.MarkIfInputEquals(Enums.Color.Illegal, positions, i + 1);
                }
                var digits = Enumerable.Range(0, limit).Select(i => (i + 1).ToString() + "... ");
                _displayer.SetDescription(string.Join(" ", digits));
            };
        }

        public void ExplainLast()
        {
            SetupDisplay();
            ExplainN(_value - 1)();
            _displayer.Mark(Enums.Color.Legal, _position, _value);
            _displayer.SetDescription(ExplanationKey("last"), _displayer.Description, _value);
        }
    }
}
