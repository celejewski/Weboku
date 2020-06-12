using System;
using UI.BlazorWASM.Filters;

namespace UI.BlazorWASM.Providers
{
    public class FilterProvider
    {
        public IFilter Filter { get; private set; } = new SelectedValueFilter(1);

        public event Action OnChanged;

        public void SetFilter(IFilter filter)
        {
            Filter = filter;
            OnChanged?.Invoke();
        }
    }
}
