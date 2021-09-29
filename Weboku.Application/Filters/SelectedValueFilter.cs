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

        public FilterOption IsFiltered(DomainFacade domainFacade, Position pos)
        {
            if (_value == Value.None)
            {
                return FilterOption.None;
            }

            if (domainFacade.GetValue(pos) == _value)
            {
                return FilterOption.Primary;
            }

            if (domainFacade.HasCandidate(pos, _value))
            {
                return FilterOption.Secondary;
            }

            return FilterOption.None;
        }
    }
}