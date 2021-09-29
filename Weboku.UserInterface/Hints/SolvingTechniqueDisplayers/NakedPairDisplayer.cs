using System;
using System.Collections.Generic;
using System.Linq;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public class NakedPairDisplayer : NakedSubsetDisplayer
    {
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly Value _value1;
        private readonly Value _value2;

        private readonly IEnumerable<House> _houses;
        private readonly List<Position> _positionsInHouses;

        public NakedPairDisplayer(Informer informer, Displayer displayer, NakedPair nakedPair)
            : base(informer, displayer, nakedPair)
        {
            _locKey = "naked-pair";
            _pos1 = nakedPair.Positions.ElementAt(0);
            _pos2 = nakedPair.Positions.ElementAt(1);
            _value1 = nakedPair.Values.ElementAt(0);
            _value2 = nakedPair.Values.ElementAt(1);

            _houses = HintsHelper.GetHouses(_positions);


            _positionsInHouses = new List<Position>();
            foreach (var house in _houses)
            {
                _positionsInHouses.AddRange(HintsHelper.GetPositionsInHouse(_pos1, house));
            }

            _explanationSteps.AddRange(new Action[]
            {
                Explain1,
                Explain2,
                Explain3,
                Explain4,
                Explain5,
            });
        }

        private void SetupDisplayer()
        {
            _displayer.SetTitle(TitleKey);
            _displayer.HighlightHouses(_pos1, _houses);
        }

        public override void DisplaySolution()
        {
            var _housesFormated = _displayer.Format(_houses, _pos1);
            base.DisplaySolution();
            SetupDisplayer();
            _displayer.SetDescription(DescriptionKey, _pos1, _pos2, _value1, _value2, _housesFormated);
        }

        private void Explain1()
        {
            SetupDisplayer();
            _displayer.Mark(Color.Legal, _positions, _values);
            _displayer.SetDescription(ExplanationKey(1), _pos1, _pos2, _value1, _value2);
        }

        private void Explain2()
        {
            SetupDisplayer();
            _displayer.MarkIfHasCandidate(Color.Illegal, _positionsInHouses, _value1);
            _displayer.Mark(Color.Legal, _pos1, _value1);
            _displayer.SetDescription(ExplanationKey(2), _value1, _pos1);
        }

        private void Explain3()
        {
            SetupDisplayer();
            _displayer.MarkIfHasCandidates(Color.Illegal, _positionsInHouses, _values);
            _displayer.Mark(Color.Legal, _pos1, _value1);
            _displayer.Mark(Color.Legal, _pos2, _value2);
            _displayer.SetDescription(ExplanationKey(3), _value2, _pos2);
        }

        private void Explain4()
        {
            SetupDisplayer();
            _displayer.MarkIfHasCandidates(Color.Illegal, _positionsInHouses, _values);
            _displayer.Mark(Color.Legal, _pos1, _value2);
            _displayer.Mark(Color.Legal, _pos2, _value1);
            _displayer.SetDescription(ExplanationKey(4), _value2, _pos1, _value1, _pos2);
        }

        private void Explain5()
        {
            var _housesFormated = _displayer.Format(_houses, _pos1);
            SetupDisplayer();
            _displayer.MarkIfHasCandidates(Color.Illegal, _positionsInHouses, _values);
            _displayer.Mark(Color.Legal, _positions, _values);
            _displayer.SetDescription(ExplanationKey(5), _value1, _value2, _housesFormated);
        }
    }
}