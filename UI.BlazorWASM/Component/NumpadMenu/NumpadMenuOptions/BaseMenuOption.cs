using System.Threading.Tasks;
using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public abstract class BaseMenuOption : INumpadMenuItem, ICommand
    {
        private readonly ICommand _command;
        private readonly SelectableMenuItemContainer _selectableMenuItemContainer;

        protected BaseMenuOption(ICommand command, SelectableMenuItemContainer selectableMenuItemContainer = null)
        {
            _command = command;
            _selectableMenuItemContainer = selectableMenuItemContainer;
        }

        public abstract bool IsDimmed { get; }
        public abstract bool IsSelectable { get; }

        public virtual string Tooltip => "tooltip__not-found";

        public async Task Execute()
        {
            if (IsSelectable)
            {
                _selectableMenuItemContainer?.SelectItem(this);
            }

            await _command.Execute();
        }
    }
}