using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class UndoMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly GridHistoryProvider _gridHistoryManager;

        public UndoMenuItem(GridHistoryProvider gridHistoryManager, UndoCommand command)
            : base(command)
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public override bool IsDimmed => !_gridHistoryManager.CanUndo;

        public override bool IsSelectable => false;

        public string Label => "numpad-undo__label";
        public override string Tooltip => "numpad-undo__tooltip";
    }
}
