using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Filters
{
    public class SelectedValueFilter : IFilter
    {
        private readonly InputValue _value;

        public SelectedValueFilter(int value)
        {
            _value = (InputValue) value;
        }

        public FilterOption IsFiltered(IGridProvider gridProvider, int x, int y)
        {
            if( _value == InputValue.Empty)
            {
                return FilterOption.None;
            }

            if (gridProvider.GetValue(x, y) == _value)
            {
                return FilterOption.Primary;
            }

            if (gridProvider.HasCandidate(x, y, _value))
            {
                return FilterOption.Secondary;
            }

            return FilterOption.None;
        }
    }
}
