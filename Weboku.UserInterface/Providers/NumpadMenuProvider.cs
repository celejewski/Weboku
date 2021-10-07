using System;
using Weboku.UserInterface.Component.NumpadMenu;
using Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions;

namespace Weboku.UserInterface.Providers
{
    public class NumpadMenuProvider : IProvider
    {
        public SelectableMenuItemContainer ActionContainer { get; } = new();
        public SelectableMenuItemContainer FilterContainer { get; } = new();
        public SelectableMenuItemContainer ColorContainer { get; } = new();

        public event Action OnLanguageChanged;

        public bool IsSelected(INumpadMenuItem selectableMenuItemContainer)
        {
            return ActionContainer.SelectedItem == selectableMenuItemContainer
                   || FilterContainer.SelectedItem == selectableMenuItemContainer
                   || ColorContainer.SelectedItem == selectableMenuItemContainer;
        }

        public NumpadMenuProvider()
        {
            ActionContainer.OnChanged += OnLanguageChanged;
            FilterContainer.OnChanged += OnLanguageChanged;
            ColorContainer.OnChanged += OnLanguageChanged;
        }
    }
}