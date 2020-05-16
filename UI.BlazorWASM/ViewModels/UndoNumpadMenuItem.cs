using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.ViewModels
{
    public class UndoNumpadMenuItem : INumpadMenuLabel
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public UndoNumpadMenuItem(IGridHistoryManager gridHistoryManager)
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public bool IsDimmed => _gridHistoryManager.CanUndo;

        public bool IsSelectable => false;

        public string Label => "Undo";

        public bool CanExecute => _gridHistoryManager.CanUndo;

        public void Execute() => _gridHistoryManager.Undo();
    }
}
