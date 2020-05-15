using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.ViewModels
{
    public class RedoNumpadMenuItem : INumpadMenuItem
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public RedoNumpadMenuItem(IGridHistoryManager gridHistoryManager)
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public bool IsDimmed => _gridHistoryManager.CanRedo;

        public bool IsSelectable => false;

        public string Label => "Redo";

        public bool CanExecute => _gridHistoryManager.CanRedo;

        public void Execute() => _gridHistoryManager.Redo();
    }
}
