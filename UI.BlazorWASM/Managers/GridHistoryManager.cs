using Core.Data;
using System;
using System.Collections.Generic;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Managers
{
    public class GridHistoryManager : IGridHistoryManager
    {
        private readonly IGridProvider _gridProvider;

        public GridHistoryManager(IGridProvider gridProvider)
        {
            _gridProvider = gridProvider;
        }

        private readonly Stack<IGridV2> _previousStates = new Stack<IGridV2>();
        private readonly Stack<IGridV2> _nextStates = new Stack<IGridV2>();
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

        public void Redo()
        {
            if( CanRedo )
            {
                _previousStates.Push(_gridProvider.Grid.Clone());
                _gridProvider.Grid = _nextStates.Pop();
                Changed();
            }
        }

        public void Save()
        {
            _previousStates.Push(_gridProvider.Grid.Clone());
            ClearRedo();
            Changed();
        }

        public void Undo()
        {
            if( CanUndo )
            {
                _nextStates.Push(_gridProvider.Grid.Clone());
                _gridProvider.Grid = _previousStates.Pop();
                Changed();
            }
        }
    }
}
