using System;
using UI.BlazorWASM.Component.NumpadMenu;

namespace UI.BlazorWASM.Providers
{
    public class NumpadMenuProvider : IProvider
    {
        public event Action OnChanged;

        public INumpadMenuItem SelectedItem { get; private set; }

        public void SelectItem(INumpadMenuItem selected)
        {
            SelectedItem = selected;
            OnChanged?.Invoke();
        }
    }
}
