using Core;
using Core.Data;

namespace UI.BlazorWASM.Filters
{
    public class SelectedValueFilter : IFilter
    {
        private readonly InputValue _value;

        public SelectedValueFilter(InputValue value)
        {
            _value = value;
        }

        public FilterOption IsFiltered(DomainFacade gridProvider, Position pos)
        {
            if( _value == InputValue.None )
            {
                return FilterOption.None;
            }

            if( gridProvider.GetInputValue(pos) == _value )
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
