﻿using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Filters
{
    public class SelectedValueFilter : IFilter
    {
        private readonly InputValue _value;

        public SelectedValueFilter(InputValue value)
        {
            _value = value;
        }

        public FilterOption IsFiltered(IGridProvider gridProvider, Position pos)
        {
            if( _value == InputValue.None )
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
