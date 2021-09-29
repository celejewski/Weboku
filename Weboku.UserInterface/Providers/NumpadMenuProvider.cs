using System;
using Weboku.UserInterface.Component.NumpadMenu;
using Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions;

namespace Weboku.UserInterface.Providers
{
    public class NumpadMenuProvider : IProvider
    {
        public SelectableMenuItemContainer ActionContainer { get; } = new SelectableMenuItemContainer();
        public SelectableMenuItemContainer FilterContainer { get; } = new SelectableMenuItemContainer();
        public SelectableMenuItemContainer ColorContainer { get; } = new SelectableMenuItemContainer();

        public event Action OnChanged;

        public INumpadMenuItem SelectedItem { get; private set; }

        public bool IsSelected(INumpadMenuItem selectableMenuItemContainer)
        {
            return ActionContainer.SelectedItem == selectableMenuItemContainer
                   || FilterContainer.SelectedItem == selectableMenuItemContainer
                   || ColorContainer.SelectedItem == selectableMenuItemContainer;
        }

        public void SelectItem(INumpadMenuItem selected)
        {
            SelectedItem = selected;
            OnChanged?.Invoke();
        }

        public NumpadMenuProvider()
        {
            ActionContainer.OnChanged += () => OnChanged();
            FilterContainer.OnChanged += () => OnChanged();
            ColorContainer.OnChanged += () => OnChanged();
        }
    }
}