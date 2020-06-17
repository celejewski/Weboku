using Core.Data;
using System;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FullHouse : BaseSolvingTechnique
    {
        private readonly Position _position;
        private readonly InputValue _value;

        private House _house;
        public FullHouse(Position position, InputValue value) 
            : base("Full House")
        {
            _position = position;
            _value = value;

            _explanationSteps.Add(Explain1);

            for( int i = 0; i < (int) (_value-1); i++ )
            {
                var explain = ExplainN((InputValue)i);
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
            _house = HintsHelper.FindHouse(_position,
                positions => positions.Where(pos => !informer.HasValue(pos))
                                      .Count() == 1
                );

            displayer.SetTitle(_title);
            displayer.HighlightHouse(_position, _house);
        }
        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);

            displayer.SetDescription($"In {Displayer.Format(_house, _position)} cell {_position} is the last one without value.");
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

            displayer.SetDescription($"In {Displayer.Format(_house, _position)} there is only one cell without set value. Let's find out what is the value by listing digits from 1 to 9...");
        }

        public Action<Displayer, Informer> ExplainN(InputValue n)
        {
            return (displayer, informer) =>
            {
                SetupDisplay(displayer, informer);
                var positions = HintsHelper.GetPositionsInHouse(_position, _house);
                var limit = (int) n + 1;
                for( int i = 0; i < limit; i++ )
                {
                    displayer.MarkIfInputEquals(Enums.Color.Illegal, positions, (InputValue)(i+1));
                }
                var digits = Enumerable.Range(0, limit).Select(i => (i + 1).ToString() + "... ");
                displayer.SetDescription(string.Join(" ", digits));
            };
        }

        public void ExplainLast(Displayer displayer, Informer informer)
        {
            SetupDisplay(displayer, informer);
            ExplainN((InputValue) (_value - 1))(displayer, informer);
            displayer.Mark(Enums.Color.Legal, _position, _value);
            displayer.SetDescription($"{displayer.Description} Bingo! Value {_value:D} is the one.");
        }
    }
}
