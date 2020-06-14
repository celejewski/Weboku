using Core.Data;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedCandidatesPointing : ISolvingTechnique
    {
        private readonly int _block;
        private readonly InputValue _inputValue;
        private readonly IEnumerable<Position> _positionsToRemoveFrom;

        public LockedCandidatesPointing(int block, InputValue inputValue, IEnumerable<Position> positionToRemoveFrom)
        {
            _block = block;
            _inputValue = inputValue;
            _positionsToRemoveFrom = positionToRemoveFrom;
        }
        public bool CanExecute(Informer informer)
        {
            return _positionsToRemoveFrom.Any(pos => informer.HasCandidate(pos, _inputValue));
        }

        public void Display(Displayer displayer, Informer informer)
        {
            displayer.SetTitle("Locked Candidates Pointing");
            displayer.SetDescription($"Pointing");

            var positionsInHouse = HintsHelper.GetPositionsInHouse(_positionsToRemoveFrom.First(), IsRowOrCol());
            var positionsWithCandidate = positionsInHouse.Where(pos => informer.HasCandidate(pos, _inputValue));
            
            displayer.Mark(Enums.Color.Info, positionsWithCandidate);
            displayer.Mark(Enums.Color.Legal, positionsWithCandidate, _inputValue);
            displayer.Mark(Enums.Color.Illegal, _positionsToRemoveFrom, _inputValue);
            displayer.HighlightHouse(_positionsToRemoveFrom.First(), IsRowOrCol());
            displayer.SetValueFilter(_inputValue);
        }

        public void Execute(Executor executor)
        {
            executor.RemoveCandidate(_inputValue, _positionsToRemoveFrom);
        }

        private House IsRowOrCol()
        {
            return _positionsToRemoveFrom.First().X == _positionsToRemoveFrom.Last().X ? House.Col : House.Row;
        }
    }
}
