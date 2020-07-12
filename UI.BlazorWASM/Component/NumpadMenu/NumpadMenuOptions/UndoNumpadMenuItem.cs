using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class UndoNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public UndoNumpadMenuItem(IGridHistoryManager gridHistoryManager, UndoCommand command)
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
