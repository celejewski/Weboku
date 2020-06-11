using Core.Data;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Filters
{
    public class SelectedValueFilter : IFilter
    {
        private readonly int _value;

        public SelectedValueFilter(int value)
        {
            _value = value;
        }

        public FilterOption IsFiltered(ICell cell)
        {
            if (cell.Input.Value == _value)
            {
                return FilterOption.Primary;
            }

            if (cell.Candidates.ContainsKey(_value))
            {
                return FilterOption.Secondary;
            }

            return FilterOption.None;
        }
    }
}
