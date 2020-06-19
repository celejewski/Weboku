using System;

namespace UI.BlazorWASM.Component.NumpadMenu
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
