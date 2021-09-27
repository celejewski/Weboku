using System;
using Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions;

namespace Weboku.UserInterface.Component.NumpadMenu
{
    public class SelectableMenuItemContainer
    {
        public event Action OnChanged;

        public INumpadMenuItem SelectedItem { get; private set; }

        public void SelectItem(INumpadMenuItem selected)
        {
            SelectedItem = selected;
            OnChanged?.Invoke();
        }

        public void DeselectItem()
        {
            SelectedItem = null;
        }
    }
}