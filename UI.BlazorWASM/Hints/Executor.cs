﻿using Core.Data;
using System.Collections.Generic;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    /// <summary>
    /// Contains method for executing ISolvingTechnique.
    /// </summary>
    public class Executor
    {
        private readonly IGridProvider _gridProvider;

        public Executor(IGridProvider gridProvider)
        {
            _gridProvider = gridProvider;
        }

        public void ClearCandidates(IEnumerable<Position> positions)
        {
            foreach( var pos in positions )
            {
                _gridProvider.ClearCandidates(pos.X, pos.Y);
            }
        }
        public void RemoveCandidate(InputValue value, Position position) 
        {
            _gridProvider.RemoveCandidate(position.X, position.Y, value);
        }
        public void RemoveCandidates(InputValue value, IEnumerable<Position> positions) 
        {
            foreach( var position in positions )
            {
                RemoveCandidate(value, position);
            }
        }
        public void AddCandidate(InputValue value, Position position) 
        {
            _gridProvider.AddCandidate(position.X, position.Y, value);
        }
        public void AddCandidates(InputValue value, IEnumerable<Position> positions) 
        {
            foreach( var pos in positions )
            {
                AddCandidate(value, pos);
            }
        }
        public void SetInput(InputValue value, Position position) => _gridProvider.SetValue(position.X, position.Y, value);
        public void FillAllLegalCandidates() => _gridProvider.FillAllLegalCandidates();
    }
}