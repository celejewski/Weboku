using Core.Data;
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

        public void RemoveCandidate(InputValue value, Position position) 
        {
            _gridProvider.RemoveCandidate(position.X, position.Y, value);
        }
        public void RemoveCandidate(InputValue value, IEnumerable<Position> positions) 
        {
            foreach( var position in positions )
            {
                RemoveCandidate(value, position);
            }
        }
        public void AddCandidate(InputValue value, Position position) { }
        public void AddCandidate(InputValue value, IEnumerable<Position> positions) { }
        public void SetInput(InputValue value, Position position) => _gridProvider.SetValue(position.X, position.Y, value);
        public void FillAllLegalCandidates() => _gridProvider.FillAllLegalCandidates();
    }
}
