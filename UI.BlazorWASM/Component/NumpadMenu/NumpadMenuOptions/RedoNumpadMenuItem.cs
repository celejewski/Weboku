using System.Threading.Tasks;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Component.NumpadMenu
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

        public Task Execute()
        {
            _gridHistoryManager.Redo();
            return Task.CompletedTask;
        }
    }
}
