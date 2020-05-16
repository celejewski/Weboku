using Core.Data;
using System;

namespace UI.BlazorWASM.Managers
{
    public class GridHistoryStub : IGridHistoryManager
    {
        public bool CanUndo => false;

        public bool CanRedo => false;

        public event Action OnChanged;

        public void ClearRedo() { }
        public void ClearUndo() { }
        public void Redo() { }
        public void Save() { }
        public void Undo() { }
    }
}
