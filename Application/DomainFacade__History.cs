using System;

namespace Application
{
    public sealed partial class DomainFacade
    {
        public bool CanRedo => _historyManager.CanRedo;
        public bool CanUndo => _historyManager.CanUndo;
        public event Action OnHistoryChanged
        {
            add { _historyManager.OnChanged += value; }
            remove { _historyManager.OnChanged -= value; }
        }

        public void Undo()
        {
            if( _historyManager.CanUndo )
            {
                _grid = _historyManager.Undo(Grid);
                ValueAndCandidateChanged();
            }
        }

        public void Redo()
        {
            if( _historyManager.CanRedo )
            {
                _grid = _historyManager.Redo(Grid);
                ValueAndCandidateChanged();
            }
        }
    }
}
