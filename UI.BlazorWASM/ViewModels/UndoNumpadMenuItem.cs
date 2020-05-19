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
        public bool IsDimmed => !_gridHistoryManager.CanUndo;

        public bool IsSelectable => false;

        public string Label => "Undo";

        public Task Execute()
        {
            _gridHistoryManager.Undo();
            return Task.CompletedTask;
        }
    }
}
