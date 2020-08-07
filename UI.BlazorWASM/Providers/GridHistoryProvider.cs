using Core.Data;
using System;
using System.Collections.Generic;

namespace UI.BlazorWASM.Providers
{
    public class GridHistoryProvider
    {
        private readonly IGridProvider _gridProvider;

        public GridHistoryProvider(IGridProvider gridProvider)
        {
            _gridProvider = gridProvider;
        }

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
