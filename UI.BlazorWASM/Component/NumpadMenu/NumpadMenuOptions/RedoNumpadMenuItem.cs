using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class RedoNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public RedoNumpadMenuItem(CommandProvider commandProvider, IGridHistoryManager gridHistoryManager)
            :base(commandProvider.Redo())
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public override bool IsDimmed => !_gridHistoryManager.CanRedo;

        public override bool IsSelectable => false;

        public string Label => "Redo";
    }
}
