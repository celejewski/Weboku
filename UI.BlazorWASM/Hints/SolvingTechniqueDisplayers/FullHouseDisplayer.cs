using Core.Data;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core.Tokens;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class FullHouseDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Position _position;
        private readonly InputValue _value;

        private readonly House _house;
        private readonly IEnumerable<Position> _positionsInHouse;
        public FullHouseDisplayer(Informer informer, Displayer displayer, FullHouse fullHouse)
            : base(informer, displayer, fullHouse, "full-house")
        {
            _position = fullHouse.Position;
            _value = fullHouse.Value;

            _house = HintsHelper.HouseFirstOrDefault(_position,
                positions => positions.Where(pos => !_informer.HasValue(pos))
                                      .Count() == 1
                );

            _positionsInHouse = HintsHelper.GetPositionsInHouse(_position, _house);

            _explanationSteps.Add(Explain01);
            _explanationSteps.Add(Explain02);
            _explanationSteps.Add(Explain03);
            for( int i = 0; i < _value - 1; i++ )
            {
                var explain = ExplainN(i);
                _explanationSteps.Add(explain);
            }
            _explanationSteps.Add(ExplainLast);
        }

        private void SetupDisplay()
        {

            _displayer.SetTitle(TitleKey);
            _displayer.HighlightHouse(_position, _house);
        }
        public override void DisplaySolution()
        {
            SetupDisplay();

            _displayer.SetDescription(DescriptionKey, _displayer.Format(_house, _position), _position);
            _displayer.MarkCells(Enums.Color.Illegal, _positionsInHouse);
            _displayer.MarkCell(Enums.Color.Legal, _position);
            _displayer.SetValueFilter(_value);
        }

        public void Explain01()
        {
            SetupDisplay();

            _displayer.SetDescription(ExplanationKey("01"), _displayer.Format(_house, _position));
            _displayer.SetValueFilter(InputValue.Empty);
        }

        public void Explain02()
        {
            SetupDisplay();

            _displayer.SetDescription(ExplanationKey("02"), _displayer.Format(_house, _position));
            _displayer.MarkCell(Enums.Color.Legal, _position);
            _displayer.SetValueFilter(InputValue.Empty);
        }
        public void Explain03()
        {
            SetupDisplay();

            _displayer.SetDescription(ExplanationKey("03"), _displayer.Format(_house, _position));
            _displayer.MarkCells(Enums.Color.Illegal, _positionsInHouse);
            _displayer.MarkCell(Enums.Color.Legal, _position);
            _displayer.SetValueFilter(InputValue.Empty);
        }

        public Action ExplainN(InputValue n)
        {
            return () =>
            {
                SetupDisplay();
                var limit = n + 1;
                for( int i = 0; i < limit; i++ )
                {
                    var value = i + 1;
                    var pos = value != _value
                    ? _positionsInHouse.First(pos => _informer.GetValue(pos) == value)
                    : _position;
                    _displayer.MarkCell(Enums.Color.Illegal, pos);
                }
                var digits = Enumerable.Range(0, limit).Select(i => (i + 1).ToString() + "... ");
                _displayer.SetDescription(string.Join(" ", digits));
                _displayer.SetValueFilter(limit);
            };
        }

        public void ExplainLast()
        {
            SetupDisplay();
            ExplainN(_value - 1)();
            _displayer.MarkCell(Enums.Color.Legal, _position);
            _displayer.SetDescription(ExplanationKey("last"), _displayer.Description, _value);
        }
    }
}
