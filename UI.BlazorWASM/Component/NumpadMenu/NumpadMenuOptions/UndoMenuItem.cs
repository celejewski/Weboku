using Application;
using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class UndoMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly DomainFacade _gridHistoryManager;

        public UndoMenuItem(DomainFacade gridHistoryManager, UndoCommand command)
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