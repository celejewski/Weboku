using System.Threading.Tasks;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class RedoNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public RedoNumpadMenuItem(RedoCommand command, IGridHistoryManager gridHistoryManager)
            :base(command)
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public override bool IsDimmed => !_gridHistoryManager.CanRedo;

        public override bool IsSelectable => false;

        public string Label => "Redo";
    }
}
