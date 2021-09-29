using Weboku.Application;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class UndoMenuItem : NumpadMenuLabel
    {
        private readonly DomainFacade _gridHistoryManager;

        public UndoMenuItem(MenuOptionSettings menuOptionSettings, DomainFacade gridHistoryManager)
            : base(menuOptionSettings)
        {
            _gridHistoryManager = gridHistoryManager;
        }

        public override bool IsDimmed => !_gridHistoryManager.CanUndo;

        public override bool IsSelectable => false;

        public string Label => "numpad-undo__label";
        public override string Tooltip => "numpad-undo__tooltip";
    }
}