using Core.Data;
using System.Collections.Generic;

namespace Core.Managers
{
    public class GridHistoryManager : IGridHistoryManager
    {
        private Grid _attached;

        private readonly Stack<Grid> _previousStates = new Stack<Grid>();
        private readonly Stack<Grid> _nextStates = new Stack<Grid>();
        public bool IsAttached { get => _attached != null; }
        public bool CanUndo { get => _previousStates.Count > 0; }
        public bool CanRedo { get => _nextStates.Count > 0; }

        public void AttachTo(Grid grid)
        {
            _attached = grid;
        }

        public void Save()
        {
            _previousStates.Push((Grid) _attached.Clone());
            _nextStates.Clear();
        }

        public void Undo()
        {
            if( CanUndo )
            {
                var current = (Grid) _attached.Clone();
                _nextStates.Push(current);

                var previous = _previousStates.Pop();
                _attached.AssignFrom(previous);
            }
        }

        public void Redo()
        {
            if( CanRedo )
            {
                var current = (Grid) _attached.Clone();
                _previousStates.Push(current);

                var next = _nextStates.Pop();
                _attached.AssignFrom(next);
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
