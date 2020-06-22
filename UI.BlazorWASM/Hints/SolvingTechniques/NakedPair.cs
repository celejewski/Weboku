using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NakedPair : NakedSubset
    {
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value1;
        private readonly InputValue _value2;

        private readonly string _housesFormated;
        private readonly IEnumerable<House> _houses;
        private readonly List<Position> _positionsInHouses;

        public NakedPair(IEnumerable<Position> positions, IEnumerable<InputValue> values)
            :base(positions, values)
        {
            _locKey = "naked-pair";
            _pos1 = positions.ElementAt(0);
            _pos2 = positions.ElementAt(1);
            _value1 = values.ElementAt(0);
            _value2 = values.ElementAt(1);
            
            _houses = HintsHelper.GetHouses(_positions);
            _housesFormated = string.Join(" and ", _houses.Select(house => Displayer.Format(house, _pos1)));

            _positionsInHouses = new List<Position>();
            foreach( var house in _houses )
            {
                _positionsInHouses.AddRange(HintsHelper.GetPositionsInHouse(_pos1, house));
            }

            _explanationSteps.AddRange(new Action<Displayer, Informer>[]{
                Explain1,
                Explain2,
                Explain3,
                Explain4,
                Explain5,
            });
        }

        private void SetupDisplayer(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);
            displayer.HighlightHouses(_pos1, _houses);
        }
        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);
            SetupDisplayer(displayer, informer);
            displayer.SetDescription(DescriptionKey, _pos1, _pos2, _value1, _value2, _housesFormated);
        }

        private void Explain1(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            displayer.Mark(Enums.Color.Legal, _positions, _values);
            displayer.SetDescription(ExplanationKey(1), _pos1, _pos2, _value1, _value2);
        }

        private void Explain2(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positionsInHouses, _value1);
            displayer.Mark(Enums.Color.Legal, _pos1, _value1);
            displayer.SetDescription(ExplanationKey(2), _value1, _pos1);
        }

        private void Explain3(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            displayer.MarkIfHasCandidates(Enums.Color.Illegal, _positionsInHouses, _values);
            displayer.Mark(Enums.Color.Legal, _pos1, _value1);
            displayer.Mark(Enums.Color.Legal, _pos2, _value2);
            displayer.SetDescription(ExplanationKey(3), _value2, _pos2);
        }
        
        private void Explain4(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            displayer.MarkIfHasCandidates(Enums.Color.Illegal, _positionsInHouses, _values);
            displayer.Mark(Enums.Color.Legal, _pos1, _value2);
            displayer.Mark(Enums.Color.Legal, _pos2, _value1);
            displayer.SetDescription(ExplanationKey(4), _value2, _pos1, _value1, _pos2);
        }

        private void Explain5(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer, informer);
            displayer.MarkIfHasCandidates(Enums.Color.Illegal, _positionsInHouses, _values);
            displayer.Mark(Enums.Color.Legal, _positions, _values);
            displayer.SetDescription(ExplanationKey(5), _value1, _value2, _housesFormated);
        }
    }
}
