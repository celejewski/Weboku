using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Filters
{
    public class EraseFilter : IFilter
    {
        public FilterOption IsFiltered(IGridProvider gridProvider, Position pos)
        {
            return FilterOption.None;
        }
    }
}
