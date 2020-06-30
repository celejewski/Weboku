using Core.Data;
using System;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FullHouse : BaseSolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _value;

        private House _house;
        public FullHouse(Position position, InputValue value)
            : base("full-house")
        {
            _position = position;
            _value = value;

            _explanationSteps.Add(Explain1);

            for( int i = 0; i < _value - 1; i++ )
            {
                var explain = ExplainN(i);
                _explanationSteps.Add(explain);
            }
            _explanationSteps.Add(ExplainLast);
        }

        public override bool CanExecute(Informer informer)
        {
            return informer.HasCandidate(_position, _value);
        }

        private void SetupDisplay(Displayer displayer, Informer informer)
        {
            _house = HintsHelper.HouseFirstOrDefault(_position,
                positions => positions.Where(pos => !informer.HasValue(pos))
                                      .Count() == 1
                );

            displayer.SetTitle(TitleKey);
            displayer.HighlightHouse(_position, _house);
        }
        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.SetDescription(DescriptionKey, displayer.Format(_house, _position), _position);
            displayer.Mark(Enums.Color.Legal, _position, _value);
            displayer.SetValueFilter(_value);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.SetInput(_value, _position);
        }

        public void Explain1(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.SetDescription(ExplanationKey("first"), displayer.Format(_house, _position));
        }

        public Action<Displayer, Informer> ExplainN(InputValue n)
        {
            return (displayer, informer) =>
            {
                SetupDisplay(displayer, informer);
                var positions = HintsHelper.GetPositionsInHouse(_position, _house);
                var limit = n + 1;
                for( int i = 0; i < limit; i++ )
                {
                    displayer.MarkIfInputEquals(Enums.Color.Illegal, positions, i + 1);
                }
                var digits = Enumerable.Range(0, limit).Select(i => (i + 1).ToString() + "... ");
                displayer.SetDescription(string.Join(" ", digits));
            };
        }

        public void ExplainLast(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);
            ExplainN(_value - 1)(displayer, informer);
            displayer.Mark(Enums.Color.Legal, _position, _value);
            displayer.SetDescription(ExplanationKey("last"), displayer.Description, _value);
        }
    }
}
