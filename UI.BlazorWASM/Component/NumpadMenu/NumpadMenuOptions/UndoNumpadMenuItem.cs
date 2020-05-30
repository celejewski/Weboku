using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class UndoNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public UndoNumpadMenuItem(IGridHistoryManager gridHistoryManager, CommandProvider commandProvider)
            :base(commandProvider.Undo())
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public override bool IsDimmed => !_gridHistoryManager.CanUndo;

        public override bool IsSelectable => false;

        public string Label => "Undo";
    }
}
