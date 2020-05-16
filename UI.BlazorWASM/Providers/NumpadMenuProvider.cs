using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.ViewModels;

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
