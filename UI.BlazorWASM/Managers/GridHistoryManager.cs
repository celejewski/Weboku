using Core.Data;
using System.Collections.Generic;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Managers
{
    public class GridHistoryManager : IGridHistoryManager
    {
        public GridHistoryManager(ISudokuProvider sudokuProvider)
        {
            _sudokuProvider = sudokuProvider;
        }

        private readonly Stack<IGrid> _previousStates = new Stack<IGrid>();
        private readonly Stack<IGrid> _nextStates = new Stack<IGrid>();
        private readonly ISudokuProvider _sudokuProvider;

        public bool CanUndo { get => _previousStates.Count > 0; }
        public bool CanRedo { get => _nextStates.Count > 0; }

        public void Save()
        {
            _previousStates.Push(_sudokuProvider.GetGridClone());
            _nextStates.Clear();
        }

        public void Undo()
        {
            if( CanUndo )
            {
                var current = _sudokuProvider.GetGridClone();
                _nextStates.Push(current);

                var previous = _previousStates.Pop();
                _sudokuProvider.AssignFrom(previous);
            }
        }

        public void Redo()
        {
            if( CanRedo )
            {
                var current = _sudokuProvider.GetGridClone();
                _previousStates.Push(current);

                var next = _nextStates.Pop();
                _sudokuProvider.AssignFrom(next);
            }
        }

        public void ClearUndo()
        {
            _previousStates.Clear();
        }

        public void ClearRedo()
        {
            _nextStates.Clear();
        }
    }
}
