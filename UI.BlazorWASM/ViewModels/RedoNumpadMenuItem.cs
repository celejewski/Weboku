using System.Threading.Tasks;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.ViewModels
{
    public class RedoNumpadMenuItem : INumpadMenuLabel
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public RedoNumpadMenuItem(IGridHistoryManager gridHistoryManager)
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public bool IsDimmed => !_gridHistoryManager.CanRedo;

        public bool IsSelectable => false;

        public string Label => "Redo";

        public bool CanExecute => _gridHistoryManager.CanRedo;

        public async Task Execute() => _gridHistoryManager.Redo();
    }
}
