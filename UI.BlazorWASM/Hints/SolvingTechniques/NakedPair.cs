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
            _title = "Naked Pair";
            _pos1 = positions.ElementAt(0);
            _pos2 = positions.ElementAt(1);
            _value1 = values.ElementAt(0);
            _value2 = values.ElementAt(1);
            
            _houses = HintsHelper.GetHouses(_positions);
            _housesFormated = string.Join(" and ", _houses.Select(house => HintsHelper.Format(house, _pos1)));

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

        private void SetupDisplayer(Displayer displayer)
        {
            displayer.SetTitle("Naked Pair");
            displayer.HighlightHouses(_pos1, _houses);
        }
        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);
            SetupDisplayer(displayer);
            displayer.SetDescription(
                $"Because cells {_pos1} and {_pos2} have only the same two cadidates left {_value1:D} and {_value2:D}, " +
                $"you can eliminated that two candidates from all other cells {_housesFormated}."
                );
        }

        private void Explain1(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer);
            displayer.Mark(Enums.Color.Legal, _positions, _values);
            displayer.SetDescription($"In cell {_pos1} and {_pos2} are only two candidates left {_value1:D} and {_value2:D}.");
        }

        private void Explain2(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer);
            displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positionsInHouses, _value1);
            displayer.Mark(Enums.Color.Legal, _pos1, _value1);
            displayer.SetDescription($"If we place {_value1:D} in {_pos1} then we can remove some candidates...");
        }

        private void Explain3(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer);
            displayer.MarkIfHasCandidates(Enums.Color.Illegal, _positionsInHouses, _values);
            displayer.Mark(Enums.Color.Legal, _pos1, _value1);
            displayer.Mark(Enums.Color.Legal, _pos2, _value2);
            displayer.SetDescription($"... it would force {_value2:D} in {_pos2}.");
        }
        
        private void Explain4(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer);
            displayer.MarkIfHasCandidates(Enums.Color.Illegal, _positionsInHouses, _values);
            displayer.Mark(Enums.Color.Legal, _pos1, _value2);
            displayer.Mark(Enums.Color.Legal, _pos2, _value1);
            displayer.SetDescription($"If we place {_value2:D} in {_pos1} then we get another possible solution with {_value1} in {_pos2}...");
        }

        private void Explain5(Displayer displayer, Informer informer)
        {
            SetupDisplayer(displayer);
            displayer.MarkIfHasCandidates(Enums.Color.Illegal, _positionsInHouses, _values);
            displayer.Mark(Enums.Color.Legal, _positions, _values);
            displayer.SetDescription($"We don't know which option is final, but in both cases we can eliminate the remaining candidates {_value1:D} and {_value2:D} in {_housesFormated}");
        }
    }
}
