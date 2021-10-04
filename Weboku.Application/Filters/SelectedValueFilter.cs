using Weboku.Core.Data;

namespace Weboku.Application.Filters
{
    public class SelectedValueFilter : IFilter
    {
        private readonly Value _value;

        public SelectedValueFilter(Value value)
        {
            _value = value;
        }

        public FilterOption IsFiltered(Grid grid, Position position)
        {
            if (_value == Value.None)
            {
                return FilterOption.None;
            }

            if (grid.GetValue(position) == _value)
            {
                return FilterOption.Primary;
            }

            if (grid.HasCandidate(position, _value))
            {
                return FilterOption.Secondary;
            }

            return FilterOption.None;
        }
    }
}