using Weboku.Application;
using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class RedoNumpadMenuItem : NumpadMenuLabel
    {
        private readonly DomainFacade _gridHistoryManager;

        public RedoNumpadMenuItem(MenuOptionSettings menuOptionSettings, DomainFacade gridHistoryManager)
            : base(menuOptionSettings)
        {
            _gridHistoryManager = gridHistoryManager;
        }

        public override bool IsDimmed => !_gridHistoryManager.CanRedo;

        public override bool IsSelectable => false;

        public string Label => "numpad-redo__label";
        public override string Tooltip => "numpad-redo__tooltip";
    }
}