using System.Threading.Tasks;
using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public abstract class BaseMenuOption : INumpadMenuItem, ICommand
    {
        private readonly ICommand _command;
        private readonly SelectableMenuItemContainer _selectableMenuItemContainer;
        private readonly bool _isSelectable;
        private readonly string _tooltip;

        protected BaseMenuOption(MenuOptionSettings menuOptionSettings)
        {
            _command = menuOptionSettings.Command;
            _selectableMenuItemContainer = menuOptionSettings.SelectableMenuItemContainer;
            _isSelectable = menuOptionSettings.IsSelectable;
            _tooltip = menuOptionSettings.Tooltip;
        }

        public virtual bool IsDimmed => !_command.CanExecute();
        public virtual bool IsSelectable => _isSelectable;

        public virtual string Tooltip => _tooltip;

        public async Task Execute()
        {
            if (IsSelectable)
            {
                _selectableMenuItemContainer?.SelectItem(this);
            }

            await _command.Execute();
        }

        public bool CanExecute()
        {
            return _command?.CanExecute() ?? true;
        }
    }
}