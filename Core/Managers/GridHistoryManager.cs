using Core.Data;
using System.Collections.Generic;

namespace Core.Managers
{
    public class GridHistoryManager : IGridHistoryManager
    {
        private IGrid _attached;

        private readonly Stack<IGrid> _previousStates = new Stack<IGrid>();
        private readonly Stack<IGrid> _nextStates = new Stack<IGrid>();
        public bool IsAttached { get => _attached != null; }
        public bool CanUndo { get => _previousStates.Count > 0; }
        public bool CanRedo { get => _nextStates.Count > 0; }

        public void AttachTo(IGrid grid)
        {
            _attached = grid;
        }

        public void Save()
        {
            _previousStates.Push((IGrid) _attached.Clone());
            _nextStates.Clear();
        }

        public void Undo()
        {
            if( CanUndo )
            {
                var current = (IGrid) _attached.Clone();
                _nextStates.Push(current);

                var previous = _previousStates.Pop();
                _attached.AssignFrom(previous);
            }
        }

        public void Redo()
        {
            if( CanRedo )
            {
                var current = (IGrid) _attached.Clone();
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
