using Core;
using Core.Data;

namespace UI.BlazorWASM.Filters
{
    public class SelectedValueFilter : IFilter
    {
        private readonly Value _value;

        public SelectedValueFilter(Value value)
        {
            _value = value;
        }

        public FilterOption IsFiltered(DomainFacade gridProvider, Position pos)
        {
            if( _value == Value.None )
            {
                return FilterOption.None;
            }

            if( gridProvider.GetValue(pos) == _value )
            {
                return FilterOption.Primary;
            }

            if( gridProvider.HasCandidate(pos, _value) )
            {
                return FilterOption.Secondary;
            }

            return FilterOption.None;
        }
    }
}
