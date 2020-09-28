using Application.Exceptions;
using Core.Data;
using System;
using System.Collections.Generic;

namespace Application.Managers
{
    internal sealed class HistoryManager
    {
        private readonly Stack<IGrid> _previousStates = new Stack<IGrid>();
        private readonly Stack<IGrid> _nextStates = new Stack<IGrid>();
        public bool CanUndo => _previousStates.Count > 0;

        public bool CanRedo => _nextStates.Count > 0;

        public event Action OnChanged;

        private void Changed() => OnChanged?.Invoke();

        public void ClearRedo()
        {
            _nextStates.Clear();
            Changed();
        }

        public void ClearUndo()
        {
            _previousStates.Clear();
            Changed();
        }

        public IGrid Redo(IGrid currentGrid)
        {
            if( CanRedo )
            {
                _previousStates.Push(currentGrid.Clone());
                Changed();
                return _nextStates.Pop();
            }
            throw new HistoryManagerInvalidOperation($"Can not {nameof(Redo)}. There are no known next states.");
        }

        public void Save(IGrid grid)
        {
            _previousStates.Push(grid.Clone());
            ClearRedo();
            Changed();
        }

        public IGrid Undo(IGrid currentGrid)
        {
            if( CanUndo )
            {
                _nextStates.Push(currentGrid.Clone());
                Changed();
                return _previousStates.Pop();
            }
            throw new HistoryManagerInvalidOperation($"Can not {nameof(Undo)}. There are no known previous states.");
        }
    }
}
